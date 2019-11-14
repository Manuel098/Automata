using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace AutomataAB
{
    public enum TypeMessage { Normal, Warning, Error };
    static class Extend    
    {        
        public async static void AsyncClear<T>(this List<T> t) => await Task.Run(() => t.Clear());
        
    }
    //Other 
    public partial class Form1 : Form
    {
        public async static void AsyncAdd (List<string> t, string dato, TypeMessage algo = TypeMessage.Normal) => await Task.Run(() =>
        {
            string Sufix = null;
            switch (algo)
            {
                case TypeMessage.Warning:
                    Sufix = "Advertencia->";
                    break;
                case TypeMessage.Error:
                    Sufix = "Error->";
                    break;
                default:
                    Sufix = "Consola->";
                    break;
            }
            var msg = $"{Sufix}{dato}";
            t.Add(msg);
        });
        public async Task<(bool,int,TypeMessage)> IsConditonal(string input)
        {
            TypeMessage TP=TypeMessage.Normal;

            var @if = @"((?<token>((Si|SiTons)|(Tons)))\s*(?<condition>(\(\s*([A-z]+\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*\})*";                       
            var ifrecursiv = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*" + @if+@"\s*\})*";
            var ifrecursive = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*" + ifrecursiv+ @"\s*\})*";
            Regex rgx = new Regex(ifrecursive, RegexOptions.Singleline);
            MatchCollection matchCollection = Regex.Matches(input, ifrecursive);
            int line = 0;
            foreach (Match match in matchCollection)
            {
                string token = match.Groups["token"].Value.ToString().Replace(" ", null);              
                string id = match.Groups["condition"].Value.ToString().Replace(" ", null);
                if (token.Contains("Tons") || input.Contains("Tons"))
                {
                    if (!string.IsNullOrEmpty(id) || input.Contains("("))
                    {
                        line = match.Groups["token"].Index;
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La sentencia 'Tons' no puede contener argumentos,  LINEA: {line}", TP);
                        
                        break;
                    }
                }
                else if (token.Contains("Si") || input.Contains("Si"))
                {
                    if ((input.Contains("(") && !input.Contains(")")))
                    {
                        line = match.Groups["token"].Index;
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La sentencia 'Si' no tiene parentesis de cierre,  LINEA: {line}", TP);
                        
                        break;
                    }
                    else if ((!input.Contains("(") && input.Contains(")"))) 
                    {
                        line = match.Groups["token"].Index;
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La sentencia 'Si' no tiene parentesis de apertura,  LINEA: {line}", TP);
                        
                        break;
                    }
                }
                Memory variable = new Memory(token, id);
                STACK.Add(variable);
            }
            return await Task.Run(() => {
                
                var tuple = (var_: rgx.IsMatch(input), lin: line, Tipe: TP);
                return tuple;                
            });
        }

        public async Task<(bool, int, TypeMessage)> IsVar(string input)
        {
            TypeMessage TP = TypeMessage.Normal;

            var @var = @"(((?<declare>\s*(Num|Dec|Tex))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*\w\s*]*'|[\d]+)\s*))?;)|((?<declare>\s*(Num|Dec|Tex))\s+(?<id>([A-z]+\s*,\s*[A-z]+)*)s*;\s*))*$";
            Regex rgx = new Regex(@var);
            MatchCollection matchCollection = Regex.Matches(input,@var);
            int line = 0;
            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                string id = match.Groups["id"].Value.ToString().Replace(" ", null);
                dynamic _value = match.Groups["value"].Value;

                if (token.Contains("Num")) 
                {                    
                    if (_value.Contains("'")) 
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE,$"El tipo de dato Num solo sirve para numeros enteros.",TP);
                        break;
                    }         
                }
                else if (token.Contains("Tex")) 
                {
                    if (!_value.Contains("'")) 
                    {                        
                        TP = TypeMessage.Error;                     
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE,$"Se esperaba el uso de comillas simples par la variable de tipo Tex", TP);
                        break;
                    }
                }                
                Memory variable = new Memory(token, id, _value);
                STACK.Add(variable);
            }
            return await Task.Run(() => {
                var tuple = (var_: rgx.IsMatch(input), lin: line, Tipe: TP);
                return tuple;                
            });
        }
        public async Task<(bool, int, TypeMessage)> IsPrint(string input)
        {
            TypeMessage TP = TypeMessage.Normal;

            var print = @"((?<declare>\s*(Imp))\s*\((?<print>('[\s*\w\s*]*'))\s*\)\s*;)*\s*$";
            Regex rgx = new Regex(print);
            int line = 0;
            MatchCollection matchCollection = Regex.Matches(input, print);

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                if (match.Groups["print"].Value != null) 
                {
                    AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"{match.Groups["print"].Value.Replace("'", null)}");
                    Memory variable = new Memory(token);
                    STACK.Add(variable);
                }
                if (input.Contains("Imp"))
                {
                    if (!input.Contains("(") || !input.Contains(")"))
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"falta un parentesis.", TypeMessage.Error);
                    else if (!input.Contains(";"))
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"falta ';'.", TypeMessage.Error);
                  /*  else
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"'Imp' no existe en el contexto actual.", TypeMessage.Error);*/
                }
            }
            return await Task.Run(() => {
                var tuple = (var_: rgx.IsMatch(input), lin: line, Tipe: TP);
                return tuple;
            });
        }

    }
}

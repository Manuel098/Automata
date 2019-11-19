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
            return await Task.Run(() => {
                TypeMessage TP = TypeMessage.Normal;

                var @if = @"((?<token>((Si|SiTons)|(Tons)))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*\})*";
                var ifrecursiv = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*("+@if+Variable+@")*\s*\})*";
                var ifrecursive = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*" + ifrecursiv + @"\s*\})*";
                Regex rgx = new Regex(ifrecursive, RegexOptions.Singleline);
                MatchCollection matchCollection = Regex.Matches(input, ifrecursive);
                int line = 0;
                bool iscond = true;
                foreach (Match match in matchCollection) {
                    string token = match.Groups["token"].Value.ToString().Replace(" ", null);
                    string id = match.Groups["condition"].Value.ToString().Replace(" ", null);
                    iscond = match.Groups["token"].Success;
                    if (iscond) break;
                    if (input.Contains("Tons")) {
                        if (!string.IsNullOrEmpty(id) || input.Contains("(") || input.Contains(")")) {
                            line = match.Groups["token"].Index;
                            TP = TypeMessage.Error;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La sentencia 'Tons' no puede contener argumentos,  LINEA: {line}", TP);

                            break;
                        }
                        else if ((!input.Contains("{") || (!input.Contains("}")))) {

                            line = match.Groups["token"].Index;
                            TP = TypeMessage.Error;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"'Tons' no existe en el contexto actual,  LINEA: {line}", TP);
                            break;
                        }
                    }
                    else if (input.Contains("Si") || input.Contains("Si")) {
                        if ((input.Contains("(") && !input.Contains(")"))) {
                            line = match.Groups["token"].Index;
                            TP = TypeMessage.Error;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La sentencia 'Si' no tiene parentesis de cierre,  LINEA: {line}", TP);

                            break;
                        }
                        else if ((!input.Contains("(") && input.Contains(")"))) {
                            line = match.Groups["token"].Index;
                            TP = TypeMessage.Error;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La sentencia 'Si' no tiene parentesis de apertura,  LINEA: {line}", TP);

                            break;
                        }
                    }
                    if (match.Groups["token"].Success) {
                        Memory variable = new Memory(token, id);
                        STACK.Add(variable);
                    }
                }
                var tuple = (var_: iscond, lin: line, Tipe: TP);
                return tuple;                
            });
        }

        public static string Variable = @"(((?<declare>\s*(Num|Dec|Tex))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*\w\s*]*'|[\d]+)\s*))?;)|((?<declare>\s*(Num|Dec|Tex))\s+(?<id>([A-z]+\s*,\s*[A-z]+)*)s*;\s*))*$";
        public async Task<(bool, int, TypeMessage)> IsVar(string input)
        {
            TypeMessage TP = TypeMessage.Normal;

            var @var = Variable;
            Regex rgx = new Regex(@var);
            MatchCollection matchCollection = Regex.Matches(input,@var);
            int line = 0;
            bool isvar = true;
            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                string id = match.Groups["id"].Value.ToString().Replace(" ", null);
                dynamic _value = match.Groups["value"].Value;
                
                if (input.Contains("Num")) 
                {                    
                    if (_value.Contains("'")) 
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE,$"El tipo de dato Num solo sirve para numeros enteros.",TP);
                        break;
                    }
                    else if(!input.Contains(";") && (input.Contains("Si") || input.Contains("Tons") || input.Contains("SiTons"))) {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Falta ';'", TP);
                        break;
                    }
                }
                else if (input.Contains("Tex")) 
                {
                    if (!input.Contains("'")) 
                    {                        
                        TP = TypeMessage.Error;                     
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE,$"Se esperaba el uso de comillas simples par la variable de tipo Tex", TP);
                        break;
                    }
                    else if (!input.Contains(";") && (input.Contains("Si") || input.Contains("Tons") || input.Contains("SiTons"))) {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Falta ';'", TP);
                        break;
                    }
                }
                if (match.Groups["declare"].Success) {
                    Memory variable = new Memory(token, id, _value);
                    STACK.Add(variable);
                }
                
            }
            foreach (Match match in matchCollection) {
                isvar = match.Groups["declare"].Success;
                if (isvar) break;
            }
            return await Task.Run(() => {
                var tuple = (var_: isvar, lin: line, Tipe: TP);
                return tuple;                
            });
        }
        public async Task<(bool, int, TypeMessage)> IsPrint(string input)
        {            
            return await Task.Run(() => 
            {
                TypeMessage TP = TypeMessage.Normal;
                bool isprint = false;
                var print = @"((?<declare>\s*(Imp))\s*\((?<print>('[\s*\w\s*]*'))\s*\)\s*;)*\s*$";
                Regex rgx = new Regex(print);
                int line = 0;
                MatchCollection matchCollection = Regex.Matches(input, print);

                foreach (Match match in matchCollection) {
                    string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                    isprint = match.Groups["declare"].Success;
                    if (isprint) break;
                    if (match.Groups["print"].Value != null) {
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"{match.Groups["print"].Value.Replace("'", null)}");
                        Memory variable = new Memory(token);
                        STACK.Add(variable);
                    }
                    if (input.Contains("Imp")) {
                        if (!input.Contains("(") || !input.Contains(")")) {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"falta un parentesis.", TypeMessage.Error);
                        }
                        else if (!input.Contains(";")) {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"falta ';'.", TypeMessage.Error);
                        }
                        if(rgx.IsMatch(input)) 
                        {
                            isprint = true;
                        }
                        else 
                        {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"'Imp' no existe en el contexto actual.", TypeMessage.Error);
                        }
                    }
                }
                var tuple = (var_: isprint, lin: line, Tipe: TP);
                return tuple;
            });
        }

    }
}

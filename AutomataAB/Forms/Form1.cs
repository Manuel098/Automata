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
using static UnityEditor.ExpressionEvaluator;

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
        public string IfArgument { get; set; }
        public async static void AsyncAdd (List<string> t, string dato, TypeMessage algo = TypeMessage.Normal) => await Task.Run(() =>
        {
            string Sufix = null;
            switch (algo)
            {
                case TypeMessage.Warning:
                    Sufix = "ADVERTENCIA->";
                    break;
                case TypeMessage.Error:
                    Sufix = "ERROR->";
                    break;
                default:
                    Sufix = "CONSOLA->";
                    break;
            }
            var msg = $"{Sufix}{dato}";
            t.Add(msg);
        });
        
        //This analize all the text
        public async Task<(bool,int,TypeMessage)> IsConditonal(string input)=>
        await Task.Run(() => 
        {
                TypeMessage TP = TypeMessage.Normal;
            
           //     var @if = @"((?<token>((Si|SiTons)|(Tons)))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*\})*";
           //     var ifrecursiv = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*(("+@if+@")("+Variable+@"))*\s*\})*";
                var ifrecursive = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{(?<argument>\s*\w*\W*\s*)\})*";
                Regex rgx = new Regex(ifrecursive, RegexOptions.Singleline);
                MatchCollection matchCollection = Regex.Matches(input, ifrecursive);
                bool iscond = true;
                int line = 0;
                foreach (Match match in matchCollection) {
                    string token = match.Groups["token"].Value.ToString().Replace(" ", null);
                    string id = match.Groups["condition"].Value.ToString().Replace(" ", null);
                    iscond = match.Groups["token"].Success;
                    //if (iscond) break;
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
                    else if (input.Contains("Si")) {
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
                    IfArgument = match.Groups["argument"].Value.ToString();
                    MessageBox.Show(IfArgument);
                    }
                    if (match.Groups["token"].Success) 
                    {
                    string _token=null;
                    if (token.Contains("Si")) _token = "Si";
                    else if (token.Contains("SiTons")) _token = "SiTons";
                    else if (token.Contains("SiTons")) _token = "Tons";

                    Memory variable = new Memory(_token, id);
                        STACK.Add(variable);
                        break;
                    }
                }
                var tuple = (var_: iscond, lin: line, Tipe: TP);
                return tuple;                
            });

        //This analize line x line
        public async void IsConditonal(string input, int line) =>
        await Task.Run(() =>
        {
            TypeMessage TP = TypeMessage.Normal;
                if (input.Contains("Si") && !input.Contains("Tons"))
                {
                    
                }
                else if (input.Contains("{"))
                {
                    
                }
                else if (input.Contains("}"))
                {

                }
                else if (input.Contains("Tons") && !input.Contains("Si"))
                {

                }
                else if (input.Contains("Si") && input.Contains("Tons"))
                {
                    
                }            
        });


        public string runAuto(string a="   ") { return ""; }
        public static string Variable = @"(((?<declare>\s*(Num|Dec|Tex))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*\w\s*]*'|[\d]+)\s*))?;)|((?<declare>\s*(Num|Dec|Tex))\s+(?<id>([A-z]+\s*,\s*[A-z]+)*)\s*;\s*)|((?<id>[a-z]+[0-9]*)\s*:=\s*(?<value>('[\s*\w\s*]*')|([\d]+(\s*[/\+\*\-]\s*[\d\w]+)*)))\s*;\s*)*$";
        public async Task<(bool, int, TypeMessage)> IsVar(string input, int line)
        {
            TypeMessage TP = TypeMessage.Normal;

            var @var = Variable;
            Regex rgx = new Regex(@var);
            MatchCollection matchCollection = Regex.Matches(input,@var);
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
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE,$"El tipo de dato Num solo sirve para numeros enteros. Linea: {line}",TP);                        
                        break;
                    }
                    else if(!input.Contains(";")) 
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Se esperaba ';' linea {line}", TP);
                        break;
                    }
                    if (input.Contains(",,"))
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Error de sintaxis ',' inesperado. Linea: {line}", TP);
                        break;
                    }
                }
                else if (input.Contains("Tex")) 
                {
                    if (!input.Contains("'")) 
                    {                        
                        TP = TypeMessage.Error;                     
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE,$"Se esperaba el uso de comillas simples par la variable de tipo Tex. linea {line}", TP);
                        break;
                    }
                    else if (!input.Contains(";")) 
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Se esperaba ';'. Linea {line}", TP);
                        break;
                    }
                    if (input.Contains(",,"))
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Error de sintaxis ',' inesperado. Linea: {line}", TP);
                        break;
                    }                 
                }
                else if (input.Contains("Dec"))
                {
                    if (_value.Contains("'"))
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"El tipo de dato Dec solo sirve para numeros enteros. Linea: {line}", TP);
                        break;
                    }
                    else if (!input.Contains(";"))
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Se esperaba ';' linea {line}", TP);
                        break;
                    }
                    if (input.Contains(",,"))
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"Error de sintaxis ',' inesperado. Linea: {line}", TP);
                        break;
                    }
                }
                if (match.Groups["declare"].Success || match.Groups["id"].Success || match.Groups["value"].Success)
                {
                    Memory variable;
                    float _Expression;
                    bool isexp=true;
                    if((_value as string).Contains("'")) 
                    variable = new Memory(token, id, _value);
                    else 
                    {
                        isexp = Evaluate<float>(_value,out _Expression);
                        variable=isexp? new Memory(token, id, _Expression): new Memory(token, id, _value.ToString().Replace(" ", null));
                    }
                    if(isexp)
                    STACK.Add(variable);
                    else 
                    {
                        TP = TypeMessage.Error;
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"La expresion: {_value}, no es un termino matematico valido. Linea: {line}", TP);
                        break;
                    }
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
        public async Task<(bool, int, TypeMessage)> IsPrint(string input, int line)
        {            
            return await Task.Run(() => 
            {
                TypeMessage TP = TypeMessage.Normal;
                bool isprint = false;
                var print = @"((?<declare>\s*(Imp))\s*\((?<print>('[\s*\w\s*]*'))\s*\)\s*;)*\s*$";
                Regex rgx = new Regex(print);
                MatchCollection matchCollection = Regex.Matches(input, print);

                foreach (Match match in matchCollection) {
                    string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                    isprint = match.Groups["declare"].Success;
                    if (isprint) 
                    {
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"{match.Groups["print"].Value.Replace("'", null)}");
                        Memory variable = new Memory(token);
                        STACK.Add(variable);
                        break;
                    }
                    
                    if (match.Groups["print"].Value != null) {
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"{match.Groups["print"].Value.Replace("'", null)}");
                        Memory variable = new Memory(token);
                        STACK.Add(variable);
                    }
                    if (input.Contains("Imp")) {
                        if (!input.Contains("(") || !input.Contains(")")) {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"se esperaba un parentesis. Linea: {line}", TypeMessage.Error);
                        }
                        else if (!input.Contains(";")) {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"se esperaba ';' Linea: {line}", TypeMessage.Error);
                        }
                        if(rgx.IsMatch(input)) 
                        {
                            isprint = true;
                        }
                        else 
                        {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"'Imp' no existe en el contexto actual. Linea: {line}", TypeMessage.Error);
                        }
                    }
                }
                var tuple = (var_: isprint, lin: line, Tipe: TP);
                return tuple;
            });
        }
        public async Task<(bool, int, TypeMessage)> IsFor(string input, int line)
        {
            return await Task.Run(() =>
            {
                TypeMessage TP = TypeMessage.Normal;
                bool isprint = false;
                var @if = @"((?<token>((Si|SiTons)|(Tons)))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*\})*";
                var ifrecursiv = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*(" + @if + Variable + @")*\s*\})*";
                var ifrecursive = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\d*\s*[=!><]=\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*" + ifrecursiv + @"\s*\})*";
                
                Regex rgx = new Regex(@"((?<token>(Lop))\s*(?<condition>((((?<declare>\s*(Num|Dec|Tex))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*\w\s*]*'|[\d]+)\s*))?;)))) | (\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*\w\s*]*'|[\d]+)\s*))?;) \s*(?<cond>[a-z]+[0-9]*(>|<|==)\s*[0-9]\s*)\s*;\s* (?<inc>([a-z]+[0-9]*)\s*(\+\+)|(\-\-))\s*;\s*\{\s*\s*\}\s*)");
                MatchCollection matchCollection = Regex.Matches(input, ifrecursive);

                foreach (Match match in matchCollection)
                {
                    string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                    isprint = match.Groups["declare"].Success;
                    if (isprint)
                    {
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"{match.Groups["print"].Value.Replace("'", null)}");
                        Memory variable = new Memory(token);
                        STACK.Add(variable);
                        break;
                    }

                    if (match.Groups["print"].Value != null)
                    {
                        AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"{match.Groups["print"].Value.Replace("'", null)}");
                        Memory variable = new Memory(token);
                        STACK.Add(variable);
                    }
                    if (input.Contains("Imp"))
                    {
                        if (!input.Contains("(") || !input.Contains(")"))
                        {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"falta un parentesis. linea {line}", TypeMessage.Error);
                        }
                        else if (!input.Contains(";"))
                        {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"falta ';' linea {line}", TypeMessage.Error);
                        }
                        if (rgx.IsMatch(input))
                        {
                            isprint = true;
                        }
                        else
                        {
                            isprint = false;
                            AsyncAdd(CONSOLEMESSAGE.MESSAGE, $"'Imp' no existe en el contexto actual. linea {line}", TypeMessage.Error);
                        }
                    }
                }
                var tuple = (var_: isprint, lin: line, Tipe: TP);
                return tuple;
            });
        }

    }
}

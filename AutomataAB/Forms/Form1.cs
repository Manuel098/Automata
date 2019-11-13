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
    static class Extend
    {   
        public async static void AsyncClear<T>(this List<T> t) => await Task.Run(() => t.Clear());
        public async static void AsyncAdd<T>(this List<T> t, T dato) => await Task.Run(() => t.Add(dato));
    }
    //Other 
    public partial class Form1 : Form
    {
        
        public async Task<(bool,int)> IsConditonal(string input)
        {           
            var @if = @"((?<token>((Si|SiTons)|(Tons)))\s*(?<condition>(\(\s*([A-z]+\s*==\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*\})*";                       
            var ifrecursiv = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\s*==\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*"+@if+@"\s*\})*";
            var ifrecursive = @"(?<token>((Si|SiTons)|(Tons))\s*(?<condition>(\(\s*([A-z]+\s*==\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*))?\{\s*" +ifrecursiv+ @"\s*\})*";
            Regex rgx = new Regex(ifrecursive, RegexOptions.Singleline);
            MatchCollection matchCollection = Regex.Matches(input, ifrecursive);
            bool isTons = true;
            int? line = null;
            foreach (Match match in matchCollection)
            {
                string token = match.Groups["token"].Value.ToString().Replace(" ", null);              
                string id = match.Groups["condition"].Value.ToString().Replace(" ", null);
                if (token.Contains("Tons"))
                {
                    if (!string.IsNullOrEmpty(id) || token.Contains("("))
                    {
                        isTons = false;
                        line = match.Groups["token"].Index;
                        break;
                    }
                }
                Memory variable = new Memory(token, id);
                STACK.AsyncAdd(variable);
            }
            return await Task.Run(() => {
                
                var tuple = (var_: rgx.IsMatch(input), lin: 5);
                if (!isTons) 
                {                    
                    ERROR.AsyncAdd($"La sentencia 'Tons' no puede contener argumentos, ERROR LINEA: {line}");
                }

                return tuple;                
                });
        }

        public async Task<bool> IsVar(string input)
        {
            var @var = @"(((?<declare>\s*(Num|Dec|Tex))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*\w\s*]*'|[\d]+)\s*))?;)|((?<declare>\s*(Num|Dec|Tex))\s+(?<id>([A-z]*\s*,\s*[A-z]*)*)s*;\s*))*$";
            Regex rgx = new Regex(@var);
            MatchCollection matchCollection = Regex.Matches(input,@var);
            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                string id = match.Groups["id"].Value.ToString().Replace(" ", null);
                dynamic value = match.Groups["value"].Value;
                Memory variable = new Memory(token, id, value);
                STACK.AsyncAdd(variable);
            }
            return await Task.Run(() => rgx.IsMatch(input) ? true : false);
        }
        public async Task<bool> IsPrint(string input)
        {
            var print = @"((?<declare>\s*(Imp))\s*\((?<print>('[\s*\w\s*]*'))\s*\)\s*;)*$";
            Regex rgx = new Regex(print);
           // Regex regd = new Regex();
            MatchCollection matchCollection = Regex.Matches(input, print);

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                if (match.Groups["print"].Value != null)
                    ERROR.Add($"{match.Groups["print"].Value.Replace("'",null)}");
                Memory variable = new Memory(token);
                STACK.AsyncAdd(variable);                
            }
            return await Task.Run(() => rgx.IsMatch(input) ? true : false);
        }

    }
}

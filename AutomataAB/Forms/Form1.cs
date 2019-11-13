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
    }
    //Other 
    public partial class Form1 : Form
    {
        public async Task<bool> IsConditonal(string input)
        {      
            var @if = @"((Si|SiTons)\s*\(\s*([A-z]+\s*==\s*('[\s*A-z\s*]*'|[\d]+))\s*\)\s*\{\s*\})*";                       
            var ifrecursive = @"((?<token>(Si|SiTons)))\s*\(\s*((?<condition>[A-z]+\s*==\s*('[\s*A-z\s*]*'|[\d]+)))\s*\)\s*\{\s*"+@if+@"\s*\}$";
            
            Regex rgx = new Regex(ifrecursive, RegexOptions.Singleline);
            MatchCollection matchCollection = Regex.Matches(input, ifrecursive);

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["token"].Value.ToString().Replace(" ", null);
                string id = match.Groups["condition"].Value.ToString().Replace(" ", null);
                Memory variable = new Memory(token, id);
                STACK.Add(variable);
            }
            return await Task.Run(() => rgx.IsMatch(input) ? true : false);
        }

        public async Task<bool> IsVar(string input)
        {
            var @var = @"((?<declare>\s*(Num|Dec|Tex))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*('[\s*A-z\s*]*'|[\d]+)\s*))?;)*$";
            Regex rgx = new Regex(@var);
            MatchCollection matchCollection = Regex.Matches(input,@var);

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                string id = match.Groups["id"].Value.ToString().Replace(" ", null);
                dynamic value = match.Groups["value"].Value;
                Memory variable = new Memory(token, id, value);
                STACK.Add(variable);
            }
            return await Task.Run(() => rgx.IsMatch(input) ? true : false);
        }
        public async Task<bool> IsPrint(string input)
        {
            var @var = @"(((?<declare>\s*(Imp))\s*\((?<print>('[\s*A-z\s*]*')))\s*\)\s*;)*$";
            Regex rgx = new Regex(@var);
            MatchCollection matchCollection = Regex.Matches(input, @var);

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                if (match.Groups["print"].Value != null)
                    ERROR.Add($"consola->{match.Groups["print"].Value.Replace("'",null)}");
                Memory variable = new Memory(token);
                STACK.Add(variable);
            }
            return await Task.Run(() => rgx.IsMatch(input) ? true : false);
        }

    }
}

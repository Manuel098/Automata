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
    public interface IOperation
    {
        T sum<T>();
        T rest<T>();
        T div<T>();
        T multi<T>();
    }

    [Serializable]
    public struct Memory : IOperation
    {
        public string TOKEN { get; set; }
        public string ID { get; set; }
        public dynamic VALUE { get; set; }
        public enum OPlst { sum, res, mult, div }

        public Memory(string token, string id = null, dynamic value = null)
        {
            TOKEN = token;
            ID = id;
            VALUE = value;
        }

        public static implicit operator decimal(Memory m) => m.VALUE;
        public static implicit operator string(Memory m) => m.VALUE;

        public override string ToString()
        {
            StringBuilder srt = new StringBuilder();
            srt.AppendFormat("TOKEN->{0}\nID->{1}\nVALUE->{2}", TOKEN, ID, VALUE);
            return srt.ToString();
        }

        public T sum<T>()
        {
            throw new NotImplementedException();
        }

        public T rest<T>()
        {
            throw new NotImplementedException();
        }

        public T div<T>()
        {
            throw new NotImplementedException();
        }

        public T multi<T>()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// FAKE CLASS
    /// </summary>
    partial class Form1 : Form
    {

        public async Task<bool> IsConditonal(string Pattern)
        {
            Regex rgx = new Regex(@"\A\s*((?<token>(Si|SiTons)))\s*\(\s*((?<condition>[a-z]+\s*==\s*('[\s*a-z\s*]*'|[\d+])))\s*\)\s*\{\s*((Num|Dec)\s+([a-z]+[0-9]*)\s*(:=\s*\d+\s*)?;)*\s*\}$\Z");
            MatchCollection matchCollection = Regex.Matches(Pattern, @"\A\s*((?<token>(Si|SiTons)))\s*\(\s*((?<condition>[a-z]+\s*==\s*('[\s*a-z\s*]*'|[\d+])))\s*\)\s*\{\s*((Num|Dec)\s+([a-z]+[0-9]*)\s*(:=\s*\d+\s*)?;)*\s*\}$\Z");

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["token"].Value.ToString().Replace(" ", null);
                string id = match.Groups["condition"].Value.ToString().Replace(" ", null);
                //dynamic value = match.Groups["value"].Value;
                Memory variable = new Memory(token, id);
                STACK.Add(variable);
            }
            return await Task.Run(() => rgx.IsMatch(Pattern) ? true : false);
        }

        public async Task<bool> IsVar(string Pattern)
        {

            string varegex = @"\A((?<declare>\s*(Num|Dec))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*\d+\s*))?;)*$\Z";
            Regex rgx = new Regex(varegex);
            MatchCollection matchCollection = Regex.Matches(Pattern, varegex);

            foreach (Match match in matchCollection)
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                string id = match.Groups["id"].Value.ToString().Replace(" ", null);
                dynamic value = match.Groups["value"].Value;
                Memory variable = new Memory(token, id, value);
                STACK.Add(variable);
            }
            return await Task.Run(() => rgx.IsMatch(Pattern) ? true : false);
        }

    }
}

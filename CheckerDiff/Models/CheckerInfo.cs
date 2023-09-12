using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerDiff.Models
{

    public enum StrategyInfo
    {
        Path,
        Upload
    }

    public class CompareCheckerInfo
    {
        public StrategyInfo Strategy { get; set; }

        public CheckerInfo? Before { get; set; }
        public CheckerInfo? After { get; set; }

        public static CompareCheckerInfo Create([NotNull]CheckerInfo before,[NotNull] CheckerInfo after)
        {
            return new CompareCheckerInfo
            {
                Before = before,
                After = after
            };
        }
    }

    public class CheckerInfo
    {
        [Name("Canon")]
        public string? Canon { get; set; }
        public BeforeCheckerFile? Before { get; set; }
        [Name("->")]
        public string Arrow { get; set; }
        public AfterCheckerFile? After { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is CheckerInfo checkerInfo)
            {
                return this.Before.Equals(checkerInfo.Before) && this.After.Equals(checkerInfo.After);
            }

            return false;
        }

    }

    public class BeforeCheckerFile
    {
        //Canon,File,Func,StartLine,StartCol,EndLine,EndCol
        [Name("BeforeFile")]
        public string? File { get; set; }
        [Name("BeforeFunc")]
        public string? Func { get; set; }
        [Name("BeforeStartLine")]
        public int? StartLine { get; set; }
        [Name("BeforeStartCol")]
        public int? StartCol { get; set; }
        [Name("BeforeEndLine")]
        public int? EndLine { get; set; }
        [Name("BeforeEndCol")]
        public int? EndCol { get; set; }

        public override bool Equals(object? obj)
        {

            switch (obj)
            {
                case BeforeCheckerFile before:
                    return base.Equals(before);
                case AfterCheckerFile after:
                    {      
                        return this.File == after.File &&
                            this.Func == after.Func &&
                            this.StartLine == after.StartLine &&
                            this.StartCol == after.StartCol &&
                            this.EndLine == after.EndLine &&
                            this.EndCol == after.EndCol;
                    }
                default:
                    return false;
            }            
        }

    }

    public class AfterCheckerFile
    {
        //File,Func,StartLine,StartCol,EndLine,EndCol,Debug
        [Name("AfterFile")]
        public string File { get; set; }
        [Name("AfterFunc")]
        public string Func { get; set; }
        [Name("AfterStartLine")]
        public int? StartLine { get; set; }
        [Name("AfterStartCol")]
        public int? StartCol { get; set; }
        [Name("AfterEndLine")]
        public int? EndLine { get; set; }
        [Name("AfterEndCol")]
        public int? EndCol { get; set; }
        [Name("Debug")]
        public string? Debug { get; set; }


        public override bool Equals(object? obj)
        {

            switch (obj)
            {
                case BeforeCheckerFile before:
                    {
                        return this.File == before.File &&
                            this.Func == before.Func &&
                            this.StartLine == before.StartLine &&
                            this.StartCol == before.StartCol &&
                            this.EndLine == before.EndLine &&
                            this.EndCol == before.EndCol;
                    }
                case AfterCheckerFile after:
                    return base.Equals(after);
                default:
                    return false;
            }
        }
    }

}

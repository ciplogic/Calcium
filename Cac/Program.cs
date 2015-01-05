#region Usages

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cal.Core.CodeGenerator;
using Cal.Core.Definitions;
using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Lexer;
using Cal.Core.Semantic;
using Cal.Core.SimpleParser;

#endregion

namespace Cal
{
    internal static class Program
    {
        static void TestScanner()
        {
            
            var mainRb = @"Tests\PrintSomething.rb";
            var lexerB = new QuickLexer();
            var tokens = lexerB.Scan(mainRb);

            //var astPacker = new AstPacker();
            //AstNode node=  astPacker.Pack(tokens);

            var ironRubyPath = @"C:\Oss\mirah-0.0.12\examples";
            var allRubyFiles = Directory.GetFiles(ironRubyPath, "*.mirah", SearchOption.AllDirectories);
            var lexer = new QuickLexer();
            var count = 0;
            var exceptions = new Dictionary<string, Exception>();
            foreach (var rubyFile in allRubyFiles)
            {
                try
                {
                    if (lexer.Scan(rubyFile)!=null)
                        count++;
                }
                catch(Exception ex)
                {
                    exceptions[rubyFile] = ex;
                }
            }
            var failedFiles = exceptions.Keys.ToArray();
        }

        public static void Main(string[] args)
        {
			EvaluateFractalMirah ();
        }

        private static void EvaluateFractalMirah()
        {
            var assemblyRuntimePath = Path.Combine(Directory.GetCurrentDirectory(), "Cal.Runtime.dll");
            var assemblyRuntime = Assembly.LoadFile(assemblyRuntimePath);

            ReferenceResolver.Instance.ScanGlobalMethods(assemblyRuntime);

            string pathExamples = @"..\..\..\";
            var fileName = pathExamples + "fractal.cal";
            var lexer = new QuickLexer();
            var tokens = lexer.Scan(fileName);
            var parser = new BlockParser(tokens);
            var parseResult = parser.Parse();
            var defBuilder = new DefinitionsBuilder();
            var progDefinition = defBuilder.Build(parseResult);
            var semanticVisitor = new SemanticVisitor();
            semanticVisitor.Build(progDefinition);
            var codeBuilder = new CsCodeGenerator(progDefinition);
            var generatedFiles = codeBuilder.GenerateFilePack(codeBuilder.MultiFile);
            File.WriteAllText(CsCodeGenerator.MainCs, generatedFiles[CsCodeGenerator.MainCs]);
        }
    }
}
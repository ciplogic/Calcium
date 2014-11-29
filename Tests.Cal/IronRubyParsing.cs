using System.IO;
using System.Linq;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;
using NUnit.Framework;

namespace Tests.Cal
{
    [TestFixture]
    public class IronRubyParsing
    {
        [Test]
        public void LexEntireIronRubyLibrary()
        {
            var ironRubyPath = @"C:\Oss\mirah-0.0.12\examples";
            var allRubyFiles = Directory.GetFiles(ironRubyPath, "*.mirah", SearchOption.AllDirectories);
            var lexer = new QuickLexer();
            var count = 0;
            foreach (var rubyFile in allRubyFiles)
            {
            	try{
                if(lexer.Scan(rubyFile)!=null)
                    count++;
            	}catch
            	{
            		
            	}
            }
            Assert.AreEqual(allRubyFiles.Length, count);
        }

		[Test]
		public void TestParseBlock()
		{
			var fileName = @"c:\Oss\mirah-0.0.12\examples\fractal.mirah";
			var lexer = new QuickLexer();
			var tokens = lexer.Scan(fileName);
			var parser = new BlockParser (tokens);
			var parseResult = parser.Parse ();

			Assert.IsTrue(parseResult.HasErrors);
		}
		    }
}
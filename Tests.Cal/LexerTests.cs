using Cal.Core.Lexer;
using NUnit.Framework;

namespace Tests.Cal
{
    [TestFixture]
    class LexerTests
    {
        [Test]
        public void TokenizeMath()
        {
            var src = "a = 2+3";
            var tokens= QuickLexer.TokenizeText(src);
            Assert.IsNotEmpty(tokens);
        }
    }
    [TestFixture]
    class IronyLexerTests
    {
        [Test]
        public void TokenizeMath()
        {
            var src = "a = 2+3";
            var tokens = QuickLexer.TokenizeText(src);
            Assert.IsNotEmpty(tokens);
        }
    }
}

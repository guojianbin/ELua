using NUnit.Framework;

namespace ELua.Test {

    [TestFixture]
    public class ELuaTest {

        [Test]
        public void TestCase1() {
            var file = "test_plus";
            var script = @"
local b = 1
local c = 2
local a = b + c
return a
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
            parser.Generate();
            Assert.IsTrue(((LuaNumber)vm.Call(file)).value == 3);
        }

        [Test]
        public void TestCase2() {
            var file = "test_plus";
            var script = @"
b = {[1]=2,[""2""]=3}
a = {a=100,b=200,c=300}
a = {1,21,33}
a = {}
print(""hello world"")
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
            parser.Generate();
            vm.Call(file);
        }

    }

}
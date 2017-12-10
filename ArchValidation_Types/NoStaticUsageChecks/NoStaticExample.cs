namespace ArchValidation.NoStaticUsageChecks {
    /*
     * Try to add "static" keyword to class/prop/method
     * to the MyUglyEnterprise class and see what happens
     */
    public class MyUglyEnterprise {
        public  string Greetings = "Hello world!";

        public int Prop { get; set; }
        
        private static void Foo() { }
    }
    
    public static class StringExtension {
        public static bool IsEmpty(this string s) {
            return true;
        }
    }
}
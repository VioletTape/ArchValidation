namespace ArchValidation.NoStaticUsageChecks {
    /*
     * Try to add "static" keyword to class/prop/method
     * to the MyUglyEnterprise class and see what happens
     */
    public class MyUglyEnterprise {
        public static string Greetings = "Hello world!";

        public  int Prop { get; set; }
        
        private  void Foo() { }
    }
    
/*
 * But aspect [NoStatic] recognize extension methods
 * as valid usage of static modificator
 */
    public static class StringExtension {
        public static bool IsEmpty(this string s) {
            return true;
        }
    }
}
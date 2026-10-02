namespace G_NET_106_Advanced_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1: What is a generic class? Why use generics?
            // generic class is a class that got one or more type parameter <t , f> instead of fixed types 
            // the actual type applied when u create an object from the class
            // why to use: no explicit casting  , reuasability , readability ,  compile time saftey

            //Q2: Write a generic class Container<T> with Add and Get methods.
            // class created

            //Q3:What are multiple type parameters? Write Pair<TKey, TValue>.
            // allows u to define more than one template parameter for a class 
            // ex: public class Pair<TKey , Tvalue>{}

            //Q4: What is a generic method? Write Swap<T> method.
            //it is a method with a template parameter return type it can be written in a generic class or non generic class

            /* int a = 5;
             int b = 10;
             Console.WriteLine($"befor swap a={a}, b= {b}");
             swap(ref a,ref b);
             Console.WriteLine($"after swap a={a} , b = {b}");*/

            //Q5: Write a generic method FindMax<T> that finds maximum value

            /*  int[] list = { 1, 2, 3 , 4 , 5};
              Console.WriteLine(FindMax(list));*/

            //Q6: What is a generic interface? Write IRepository<T>. 
            // generic interface : it is an interface with template parameter so it can hold more than one methods type
            // when implementing this contract with class u decide the return type of the contract methods

            //wrote


            //Q7: What is the 'struct' constraint? Write an example.
            // it means that this generic template parameter must be a struct
            //ex: public class Constraint <T> where T : struct

            //Q8: What is the 'class' constraint? Write an example.
            // the generic must be a class type 
            //ex: public class Constraint <T> where T : class

            //Q9: What is the 'new()' constraint? Write an example.
            // requires that the type parameter must get a public parameterless constructor
            //ex: class Constraint <T> where T : new(){
            //public Constraint(){
            //return new t()
            //}
            //
            //}

            //Q10:  What is the interface constraint? Write an example.
            // that type implement this interface 
            // class constraint <T> where t : IComparable

            //Q11: What is the base class constraint? Write an example.
            // template parameter this value must be inherit from this parent class
            //ex: class constraint <T> where t : parent class 

            //Q12 How do you apply multiple constraints? Write an example
            // class Constraint<T> where T : struct , IComparable , new()

            // Q13: What does the 'default' keyword do in generics?
            //in the implementation of the generic type it sees the type if it string returns the default which is null
            // if it is an integer return the default => 0

            //Q14: Write a SafeList<T> that returns default when the index is invalid.
            /* var numbers  = new SafeList<int>();
             numbers.Add(1);
             numbers.Add(2);
             numbers.Add(3);

             Console.WriteLine(numbers.Get(0));
             Console.WriteLine(numbers.Get(5));*/

            //Q15: What is covariance? Explain the 'out' keyword.
            //Covariance means you can use a more derived type where a less derived type is expected. In generics
            //out on a type parameter marks it as covariant. It tells the compiler: T is used only as an output (return type), never as an input (method parameter).

            //Q16: What is contravariance? Explain the 'in' keyword.
            //Contravariance is the opposite of covariance.
            //It means you can use a less derived type where a more derived type is expected
            //in on a type parameter marks it as contravariant.
            //It tells the compiler: T is used only as an input (method parameter),
            //never as an output (return type).

            //Q17: What is the difference between covariance and contravariance?
            // the ans same as the two previous questions


            //Q18: How do static members work in generic types?
            //each closed generic type gets its own copy of the static members

            //Q19: How can you inherit from a generic class?
            // it is as the same as the regular inheritance in classes and interfaces

            //Q20: Complete Exercise - Create a generic
            //Cache<TKey, TValue>with Add, Get, Remove, Contains, and expiration support. 

            var cache = new Cache<string, int>(TimeSpan.FromSeconds(2));

            cache.Add("a", 1);                                  
            cache.Add("b", 2, TimeSpan.FromSeconds(5));         
            cache.Add("c", 3, Timeout.InfiniteTimeSpan);        

            Console.WriteLine(cache.Get("a"));                  
            Console.WriteLine(cache.Contains("b"));             
            Thread.Sleep(3000);                                 
            Console.WriteLine(cache.Contains("a"));             
            Console.WriteLine(cache.Contains("b"));             

            if (!cache.TryGet("a", out int value))
                Console.WriteLine("a is gone");                 

            cache.Remove("b");
            Console.WriteLine(cache.Contains("b"));             
            Console.WriteLine(cache.Count);




        }
        static void swap<T>(ref T a, ref T b)
        {
            T Value = a;
            a = b;
            b = Value;

        }
        static T FindMax<T>(T[] list) where T: IComparable<T> 
        {
            T max = list[0];
            foreach (T t in list) { 
             if(t.CompareTo(max) > 0)
                {
                    max = t;
                }
             
            }
            return max;

        }
    }
  
}

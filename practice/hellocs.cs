using System;

class Program{
    static void Main(string[] args){
        var i = 1;
        Console.WriteLine($"{i}");
        Console.WriteLine("Hello World!");
        Console.WriteLine("Enter your name:");
        var name = Console.ReadLine();
        Console.WriteLine("Hello " + name);
    }
}
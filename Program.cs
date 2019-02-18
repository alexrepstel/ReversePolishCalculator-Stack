using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;


//Stelios Alexandrou 2019

namespace ReversePolishCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please insert an expression:");
            String ogExpression = Console.ReadLine();
            //Calling the bridge function
            result(ogExpression);
            Console.ReadLine();
        }

        //Sends the input to the processing functions and gets response
        static void result(String expression)
        {
            //Splitting the string in different tokens by whitespace and adding to string array
            string[] splitResult = expression.Split(' ');
            //Creating a new stack
            Stack<String> stack = new Stack<String>();

            //Looping through all the tokens from the string array
            for (int j = 0; j < splitResult.Length; j++)
            {
                //Pushing all the tokens onto the stack
                stack.Push(splitResult[j]);
            }

            try
            {
                //Calling the processing function passing the stack data
                string res = recurse(stack);
                //Pushing the result back onto the stack
                stack.Push(res);
                Console.WriteLine(res);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something has gone wrong");
            }

        }


        //The processing function
        static string recurse(Stack<string> stack)
        {
            int a, b;
            //Getting the first item of the stack
            string top = stack.Pop();
            //Initialising some checkers
            bool noSymbol = false;
            bool divByZero = false;


            if (!Int32.TryParse(top, out a))
            {

                //Removing all "p" occurences from the stack
                while (top.Equals("p"))
                {
                    top = stack.Pop();
                }

                //This applies to the 7th input example from the Sample Data list
                int firstVal = 0;
                int secondVal = 0;
                for (int i = 0; i < 2; i++)
                {
                    //Checking if the character is a number
                    try
                    {
                        int numVal = Int32.Parse(top);
                        if (i == 0)
                        {
                            //Saving and removing from the stack the 1st value
                            firstVal = numVal;
                            top = stack.Pop();
                        }
                        else if (i == 1)
                        {
                            //Saving and removing from the stack the 2nd value
                            secondVal = numVal;
                            top = stack.Pop();

                        }
                        //Checker
                        noSymbol = true;
                    }
                    catch (Exception n)
                    {

                    }
                }
                //First digit still in stack, result is returned
                if (noSymbol) return firstVal.ToString() + " " + secondVal.ToString();


                //Calling the processing function again within itself to pop the value from the stack
                //and checking for input type if it's valid
                try
                {
                    a = Int32.Parse(recurse(stack));
                }
                catch (Exception e)
                {
                    return "Error: Input type not valid!";
                }



                //This applies to the 8th input example, given on the Sample Data sheet
                //and generally validating input
                if (a < 0)
                {
                    //At this point the first value is still in the stack
                    return "Error: Negative Token(" + a + ")";
                }

                //This applies to the 9th input example, given on the Sample Data sheet
                else if (a == 0)
                {
                    divByZero = true;
                }

                //This applies to the 10th input example, given on the Sample Data sheet
                if (stack.Count == 0)
                {
                    return "Error: Stack is empty!";
                }

                //Calling the processing function again within itself to pop the value from the stack
                //and checking for input type if it's valid
                try
                {
                    b = Int32.Parse(recurse(stack));
                }
                catch (Exception e)
                {
                    return "Error: Input type not valid!";
                }

                if (b == 0)
                {
                    divByZero = true;
                }




                //Checking if the token is negative
                if (b < 0)
                {
                    //At this point the first value is still in the stack
                    return ("Error: Negative Token(" + b + ")");
                }

                //Checking the operators and doing the maths accordingly
                if (top.Equals("+"))
                {
                    a = a + b;

                }
                else if (top.Equals("-"))
                {
                    a = b - a;
                }
                else if (top.Equals("*"))
                {
                    a *= b;
                }
                else if (top.Equals("/"))
                {
                    //Checks if the any of the given numbers were 0
                    if (divByZero)
                    {
                        return "Error: Cannot divide zero!";
                    }
                    else
                    {
                        a = b / a;
                    }
                }
            }
            //Returns the result
            return a.ToString();
        }

    }

}

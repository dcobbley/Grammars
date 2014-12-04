using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace GrammarAnalyzer
{
    public partial class Form1 : Form
    {
        string inputBuffer = "";
        string inputGrammar="";
        Stack myStack = null;
        int count=0;
        bool acceptFlag = false;
        Dictionary<string, List<string>> grammarDictionary;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            inputBuffer = tb_StringToTest.Text.ToString();
        }

        private void getGrammar()
        {
            string path = Directory.GetCurrentDirectory();
            inputGrammar = System.IO.File.ReadAllText(path = @"/sampleInput.txt");
        }

        private void initStack()
        {
            myStack = new Stack();
            myStack.Push("$");
            myStack.Push(createDictionary());
        }

        private string createDictionary()
        {
            getGrammar();
            //parse the incoming grammar into a dictionary.
            grammarDictionary = new Dictionary<string, List<string>>();
            string[] delimitedGrammar = parseFile();
            myStack.Push(delimitedGrammar[0]);

            foreach(string str in delimitedGrammar)
            {
                char nonTerminal;
                if(str == str.ToUpper())
                {
                    nonTerminal = Convert.ToChar(str);
                }
                else if (str != str.ToUpper())
                {
                    grammarDictionary.Add(nonTerminal, )
                    //add the elements to the dictionary
                    //grammarDictionary.Add(delimitedGrammar[0],str);
                }
            }
            return (delimitedGrammar[0]);
        }

        private string[] parseFile()
        {
            string[] temp = null;
            temp = inputGrammar.Split(',');
            return temp;
        }

        private void popStack()
        {
            //pop an element
            string element = myStack.Pop().ToString();

            //if elemenet is a variable, look at next element of inputBuffer, determine which rule to use.
            //push right hand side of rule onto stack, else reject if no match.
            if (element == element.ToUpper())
            {
                //element is a variable. See if the current input buffer char matches a character in the dictionary.
                //inputBuffer[0];
                /*foreach (KeyValuePair<string, string> kvp in grammarDictionary)
                {
                    if(kvp.Key == element)
                    {
                        //The non terminals match
                        if(kvp.Value[0] == inputBuffer[count])
                        {
                            //push the dictionary value onto the stack backwards.
                            pushStackBackwards(kvp.Value);
                        }
                    }
                }*/

            }
            //If the element is a terminal, check it matches next character in input and remove both. else reject.

            //if stack is empty and inputbuffer is not, reject.

            //if both stack and input buffer empty, accept.
        }

        public void pushStackBackwards(string toPush)
        {
            char[] arr = toPush.ToCharArray();
            Array.Reverse(arr);
            string tempString = arr.ToString();

            

        }
    }
}

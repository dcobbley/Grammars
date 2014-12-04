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
        Stack inputBuffer = null;
        string[] inputGrammar=null;
        Stack myStack = null;
        int count=0;
        bool acceptFlag = false;
        Dictionary<string, List<string>> grammarDictionary;
        string startVariable = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            loadInputBuffer();
        }

        private void loadInputBuffer()
        {
            string input = tb_StringToTest.Text.ToString();
            input = reverseString(input);
            foreach (char ch in input)
            {
                inputBuffer.Push(ch.ToString());
            }
        }

        private void getGrammar()
        {
            string path = Directory.GetCurrentDirectory();
            inputGrammar = System.IO.File.ReadAllLines(path = @"/sampleInput.txt");

            //Silly Conversion in order to get element zero from the string.
            char tempChar = Convert.ToChar(inputGrammar[0]);
            startVariable = tempChar.ToString();
        }

        private void initStack()
        {
            myStack = new Stack();
            myStack.Push("$");
            createDictionary();
            myStack.Push(startVariable);
        }

        private void createDictionary()
        {
            getGrammar();
            //parse the incoming grammar into a dictionary.
            grammarDictionary = new Dictionary<string, List<string>>();

            foreach(string str in inputGrammar)
            {
                string nonTerminal="";
                string[] tempSplit = str.Split(',');
                List<string> tempList = new List<string>();

                foreach(string tS in tempSplit)
                {
                    if(tS == tS.ToUpper())
                    {
                        //its a non terminal.
                        nonTerminal = tS;
                    }
                    else if(tS != tS.ToUpper())
                    {
                        tempList.Add(tS);
                    }
                    grammarDictionary.Add(nonTerminal, tempList);
                }
            }
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

        public string reverseString(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return(arr.ToString());
        }
    }
}

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
        bool acceptFlag = false;
        Dictionary<string, List<string>> grammarDictionary;
        string startVariable = "";
        string inputPath = "";
        System.Windows.Forms.RadioButton[] radioButtons = null; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listInputGrammarFiles();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {

        }

        private void loadInputBuffer()
        {
            inputBuffer = new Stack();
            string input = tb_StringToTest.Text.ToString();
            input = reverseString(input);
            inputBuffer.Push("$");
            foreach (char ch in input)
            {
                inputBuffer.Push(ch);
            }
        }

        private void listInputGrammarFiles()
        {
            string path = Directory.GetCurrentDirectory();
            string[] fileEntries = Directory.GetFiles(path);
            int count = 0;
            List<string> shortFileName = new List<string>();
            string reversed = "";
            string[] tempString;

            foreach(string str in fileEntries)
            {
                if (str.Contains("sample"))
                {
                    ++count;

                    //grab the string, reverse it, chop off when first \ encountered, put in new string array.
                    reversed = reverseString(str);
                    tempString = reversed.Split('\\');
                    shortFileName.Add(reverseString(tempString[0]));
                }
            }

            radioButtons = new System.Windows.Forms.RadioButton[count];
            count = 0;
            foreach (string str in shortFileName)
            {
                    radioButtons[count] = new RadioButton();
                    radioButtons[count].Text = str;
                    radioButtons[count].Location = new System.Drawing.Point( 10, 10 + count * 20);
                    this.Controls.Add(radioButtons[count]);
                    ++count;
            }

        }

        private void getGrammar()
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                inputGrammar = System.IO.File.ReadAllLines(path + @"/"+ inputPath);

                //Silly Conversion in order to get element zero from the string.
                string tempString = inputGrammar[0];
                startVariable = tempString[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                    
                    if(tS == tS.ToUpper()&& tS != "#")
                    {
                        //its a non terminal.
                        nonTerminal = tS;
                    }
                    else if(tS != tS.ToUpper() || tS == "#")
                    {
                        tempList.Add(tS);
                    }
                }
                if (nonTerminal != "")
                {
                    grammarDictionary.Add(nonTerminal, tempList);
                }
            }
        }

        private bool popStack()
        {
            //create a list to grab from the dictionary.
            List<string> tempList=null;
            //pop an element
            string element = myStack.Pop().ToString();
            //pop character from input buffer
            string buffer = inputBuffer.Peek().ToString();

            //if stack is empty and inputbuffer is not, reject.
            if (element == "$" && buffer != "$")
                return false;
            //if both stack and input buffer empty, accept.
            if (element == "$" && buffer == "$")
            {
                acceptFlag = true;
                return true;
            }

            //if elemenet is a variable, look at next element of inputBuffer, determine which rule to use.
            //push right hand side of rule onto stack, else reject if no match.
            if (element == element.ToUpper() && element != "#")
            {
                //element is a variable. See if the current input buffer char matches a character in the dictionary.
                if(grammarDictionary.ContainsKey(element))
                {
                    tempList = grammarDictionary[element];
                }
                foreach(string str in tempList)
                {
                    if (str[0].ToString() == buffer)
                        {
                            //push the rule onto the stack
                            pushString(reverseString(str));
                            return true;
                        }
                }
                return false;
                //reject
            }
            else if(element != element.ToUpper()||(element == "#" && buffer == "#"))
            {
                //If the element is a terminal, check it matches next character in input and remove both. else reject.
                if(element == buffer.ToString())
                {
                    inputBuffer.Pop();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }
        
        public void pushString(string toPush)
        {
            foreach(char str in toPush)
            {
                myStack.Push(str);
            }
        }

        public string reverseString(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            string temp = new string(arr);
            return(temp);
        }

        public void runAnalysis()
        {
            bool flag = false;
            while (true)
            {
                flag = popStack();
                if (flag && acceptFlag)
                {
                    //Win
                    MessageBox.Show("Accepted!");
                    break;
                }
                else if(!flag && !acceptFlag)
                {
                    //FML
                    MessageBox.Show("Failed to build String.");
                    break;
                }
            }
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            foreach(RadioButton rb in radioButtons)
            {
                if (rb.Checked)
                    inputPath = rb.Text.ToString();
            }
            if (inputPath != "")
            {
                loadInputBuffer();
                initStack();
                runAnalysis();
            }
            else if(inputPath == "")
            {
                MessageBox.Show("Please pick an input text file.");
            }
        }
    }
}

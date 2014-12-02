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
        string[] inputGrammar=null;
        Stack myStack = null;
        int count;
        bool acceptFlag = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myStack= new Stack();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            inputBuffer = tb_StringToTest.Text.ToString();
        }

        private void getGrammar()
        {
            string path = Directory.GetCurrentDirectory();
            inputGrammar = System.IO.File.ReadAllLines(path = @"/sampleInput.txt");
        }

        private void loadStack(int i)
        {
            getGrammar();
            //push start state into stack.
            myStack.Push(inputGrammar[i].[0]);
        }

        private void popStack()
        {
            //pop an element

            //if elemenet is a variable, look at next element of inputBuffer, determine which rule to use.
            //push right hand side of rule onto stack, else reject if no match.

            //If the element is a terminal, check it matches next character in input and remove both. else reject.

            //if stack is empty and inputbuffer is not, reject.

            //if both stack and input buffer empty, accept.
        }
    }
}

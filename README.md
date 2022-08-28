<h1>Text analysis: By Using MapReduce & Threads</h1>

<span style="color:red">The next C# code find a string pattern(Including all possible chars) in a text file, by using MapReduce algorithm and Thread tools </span>

<b>The program will get the next Inputs:</b>
</br>
</br>
<b>Text:</b> text file to analysis.
</br>
<b>Keyword:</b> the string-pattern to search in the text file
</br>
<b>ThreadsNumber:</b> How many threads will used for the search
</br>
<b>Delta:</b> The distance between letters in the search pattern
</br>
</br>
<b>The pogram will return the next Output:</b>
</br>
The program will output the locations of te keword in the txt file, in the next order:</br>
[row number,location in the row] with referring to the delta parameter. 
</br>
</br>
<b>NetaLavi.txt:</b>
</br>
[Row 0]:Neta Lavi is the Best
</br>
[Row 1]:Football player in the world
</br>
[Row 2]:Neta Lavi6 is a legend


<h3>Example 1:</h3>

<b>Input:</b>

•<b>Text:</b> NetaLavi.txt

•<b>Keyword:</b> Neta

•<b>ThreadsNumber:</b>2

•<b>Delta:</b>0

<b>Output:</b>

[0,0]
[2,0]

<h3>Example 2:</h3>

<b>Input:</b>

•<b>Text:</b> NetaLavi.txt

•<b>Keyword:</b> Ni

•<b>ThreadsNumber:</b>4

•<b>Delta:</b>7

<b>Output:</b>

[0,3],
[1,0]

<h3>Example 3:</h3>

<b>Input:</b>

•<b>Text:</b> NetaLavi.txt

•<b>Keyword:</b> al

•<b>ThreadsNumber:</b>3

•<b>Delta:</b>2

<b>Output:</b>

[0,3],
[1,5],
[2,3],
[2,14]
<pre></pre>
My program is based on The MapReduce algorithm and the Regular Expression tool.

•	<b>Regular Expression:</b> A regular expression is a sequence of characters that specifies a search pattern in the text. Usually, such patterns are used by string-searching algorithms for "find" or "find and replace" operations on strings, or for input validation. 

•	<b>The MapReduce algorithm:</b> MapReduce is a programming model and an associated implementation for processing and generating big data sets with a parallel. The MapReduce algorithm contains two important tasks, namely Map and Reduce. Mapper class takes the input and maps it. The output of the Mapper class is used as input by the Reducer class, which in turn searches for matching pairs and reduces them.


<b>Why use these two methods?</b>
1.	MapReduce implements various mathematical algorithms to divide a task into small parts and assign them to multiple systems. In technical terms, the MapReduce algorithm helps in sending the Map & Reduce tasks to appropriate servers. In my program, I perform a search process using Threads. The Threads is our multiple systems.
2.	The main use of regular expressions is to match patterns of the text so that the program can easily recognize and manipulate the text file. In our Text, we are required to fast search processing, and used regular expression Allows it.
<pre></pre>
My Algorithm includes three main steps:

<b>Step A:</b> Data pre-processing

<b>Step B:</b> The Map phase

<b>Step C:</b> The Reduce phase

<h2>Step A: Data pre-processing</h2>

in this Process, we will calculate the next four elements:

1.<b>(Regex) rx_For_Pre_Processing:</b> Calculate the Regex to Identify the Candidate's Strings
  
2.<b>(Regex) rx_For_Match:</b> Calculate the Regex Who Identify the matches String
	
3.<b>(int) CandidateStringLen:</b> Calculate the Length of the string

4.<b>(string[]) Text Lines:</b> Read the file.txt to an array. Each cell represents a row in the text file. 

<h2>Step B: The map Phase</h2>
The map task is done by means of Mapper Class.

<b>The Map phase</b> processes take the text input file and provides the string who candidate for the match by the next  representation :

(<key, Val> : <Row_In_the_text_file,  Location_In_the_row>).

The map Process include a use in Regular expression

<h2>Step C: The Reduce phase</h2>

The reduce task is done by means of Reducer Class:

The Reduce phase (searching technique) will accept the input from the Map phase as a key-value pair with Row_In_the_text_file and Location_In_the_row. 

Using searching technique, the combiner will check all the Key value pair and he will print to the screen the String how match for the Matching Regular Expression.



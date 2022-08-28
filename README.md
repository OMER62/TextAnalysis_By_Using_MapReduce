<h1>Text analysis: By Using MapReduce & Threads</h1>

<span style="color:red">The next C# code find a string pattern(Including all possible chars) in a text file, by using MapReduce algorithm and Thread tools </span>

<b>The program will get the next Inputs:</b>
</br>
</br>
<b>Txt:</b> text file to analysis.
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


Example 1:
Input:
Txt:
Keyword:
ThreadsNumber:
Delta:

Output:

[0,1]
[0,14]
[1,1]

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


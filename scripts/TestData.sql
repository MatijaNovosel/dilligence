INSERT INTO Exam
	(Naziv, SubjectID, TimeNeeded, DueDate)
VALUES
	('General programming exam', 147, 3600, CURRENT_TIMESTAMP);

INSERT INTO Question
	(Title, Content, ExamID, TypeID)
VALUES
	(
		'Javascript',
		'What is the output of this block of code?<div><br><div><div style="background-color: rgb(40, 44, 52); line-height: 19px; white-space: pre; font-family: Consolas, &quot;Courier New&quot;, monospace; color: rgb(171, 178, 191);"><br><div><span style="color: #c678dd;"> var</span>&nbsp;<span style="color: #e06c75;">numbers</span>&nbsp;<span style="color: #56b6c2;">=</span>&nbsp;[&nbsp;<span style="color: #d19a66;">1</span>,&nbsp;<span style="color: #d19a66;">2</span>,&nbsp;<span style="color: #d19a66;">3</span>,&nbsp;<span style="color: #d19a66;">4</span>,&nbsp;<span style="color: #d19a66;">5</span>&nbsp;];</div><div><span style="color: #e06c75;"> numbers</span>.<span style="color: #61afef;">sort</span>((<span style="color: #e06c75;font-style: italic;">a</span>,&nbsp;<span style="color: #e06c75;font-style: italic;">b</span>)&nbsp;<span style="color: #c678dd;">=&gt;</span>&nbsp;{&nbsp;</div><div>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #c678dd;">if</span>(<span style="color: #e06c75;">a</span>&nbsp;<span style="color: #56b6c2;">&gt;</span>&nbsp;<span style="color: #e06c75;">b</span>)&nbsp;<span style="color: #c678dd;">return</span>&nbsp;<span style="color: #d19a66;">1</span>;</div><div>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #c678dd;">else</span>&nbsp;<span style="color: #c678dd;">return</span>&nbsp;<span style="color: #56b6c2;">-</span><span style="color: #d19a66;">1</span>;</div><div> });</div><div><span style="color: #e5c07b;"> console</span>.<span style="color: #61afef;">log</span>(<span style="color: #e06c75;">numbers</span>);</div><br></div></div></div>',
		1,
		1
),
	(
		'C++ 1',
		'How would you instantiate an object of this class type?<div><br></div><div><div style="color: rgb(171, 178, 191); background-color: rgb(40, 44, 52); font-family: Consolas, &quot;Courier New&quot;, monospace; line-height: 19px; white-space: pre;"><br><div><span style="color: #c678dd;"> template</span>&nbsp;&lt;<span style="color: #c678dd;">class</span>&nbsp;<span style="color: #e5c07b;">T</span>&gt;</div><div><span style="color: #c678dd;"> class</span>&nbsp;<span style="color: #e5c07b;">Array</span>&nbsp;</div><div> {</div><div>&nbsp;&nbsp;<span style="color: #c678dd;">public:</span></div><div>&nbsp;&nbsp;T<span style="color: #c678dd;">*</span>&nbsp;array;</div><div>&nbsp;&nbsp;<span style="color: #c678dd;">int</span>&nbsp;NumberOfElements;</div><div>&nbsp;&nbsp;<span style="color: #61afef;">Array</span>(<span style="color: #c678dd;">int</span>&nbsp;<span style="color: #e06c75;font-style: italic;">n</span>);</div><div>&nbsp;&nbsp;<span style="color: #61afef;">~Array</span>()&nbsp;{&nbsp;<span style="color: #c678dd;">delete[]</span>&nbsp;array;&nbsp;</div><div> }</div><div>&nbsp;&nbsp;</div><div><span style="color: #c678dd;"> template</span>&nbsp;&lt;<span style="color: #c678dd;">class</span>&nbsp;<span style="color: #e5c07b;">T</span>&gt;</div><div><span style="color: #e5c07b;"> Array</span>&lt;<span style="color: #e5c07b;">T</span>&gt;::<span style="color: #61afef;">Array</span>(<span style="color: #c678dd;">int</span>&nbsp;n)&nbsp;</div><div> {</div><div>&nbsp;&nbsp;NumberOfElements&nbsp;<span style="color: #c678dd;">=</span>&nbsp;n;</div><div>&nbsp;&nbsp;array&nbsp;<span style="color: #c678dd;">=</span>&nbsp;<span style="color: #c678dd;">new</span>&nbsp;<span style="color: #e06c75;">T</span>[n];</div><div> }</div><br></div></div>',
		1,
		1
),
	(
		'C++ 2',
		'<div>What is the output of this block of code?</div><div><br></div><div><div style="color: rgb(171, 178, 191); background-color: rgb(40, 44, 52); font-family: Consolas, &quot;Courier New&quot;, monospace; line-height: 19px; white-space: pre;"><br><div><span style="color: #c678dd;"> template</span>&lt;<span style="color: #c678dd;">class</span>&nbsp;<span style="color: #e5c07b;">T</span>&gt;</div><div><span style="color: #c678dd;"> class</span>&nbsp;<span style="color: #e5c07b;">A</span></div><div> {</div><div>&nbsp;&nbsp;<span style="color: #c678dd;">protected:</span></div><div>&nbsp;&nbsp;&nbsp;&nbsp;T&nbsp;x;</div><div>&nbsp;&nbsp;<span style="color: #c678dd;">public:</span></div><div>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #61afef;">A</span>(<span style="color: #e5c07b;">T</span>&nbsp;<span style="color: #e06c75;font-style: italic;">x</span>)&nbsp;{&nbsp;<span style="color: #e5c07b;">this</span>-&gt;<span style="color: #e06c75;">x</span>&nbsp;<span style="color: #c678dd;">=</span>&nbsp;x;&nbsp;}</div><div>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #c678dd;">void</span>&nbsp;<span style="color: #61afef;">print</span>()</div><div>&nbsp;&nbsp;&nbsp;&nbsp;{</div><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cout&nbsp;<span style="color: #c678dd;">&lt;&lt;</span>&nbsp;x&nbsp;<span style="color: #c678dd;">&lt;&lt;</span>&nbsp;<span style="color: #98c379;">"&nbsp;"</span>;</div><div>&nbsp;&nbsp;&nbsp;&nbsp;};</div><div> };</div><div>&nbsp;&nbsp;&nbsp;&nbsp;</div><div><span style="color: #c678dd;"> int</span>&nbsp;<span style="color: #61afef;">main</span>()</div><div> {</div><div>&nbsp;&nbsp;A&nbsp;<span style="color: #61afef;">a</span>(<span style="color: #98c379;">"A"</span>);</div><div>&nbsp;&nbsp;A&nbsp;<span style="color: #61afef;">b</span>(<span style="color: #98c379;">"A"</span>);</div><div>&nbsp;&nbsp;<span style="color: #e06c75;">a</span>.<span style="color: #61afef;">print</span>();&nbsp;</div><div>&nbsp;&nbsp;<span style="color: #e06c75;">b</span>.<span style="color: #61afef;">print</span>();</div><div> }</div><br></div></div>',
		1,
		1
);

INSERT INTO Answer
	(Content, Correct, QuestionID)
VALUES
	-- Q1
	('I do not know', 0, 1),
	('Compilation error', 0, 1),
	('Array(1, 2, 3, 4, 5)', 1, 1),
	('Array(5, 4, 3, 2, 1)', 0, 1),
	-- Q2
	('Array(double) P[50];', 0, 2),
	('Array<double> P[50];', 0, 2),
	('Array P[50];', 0, 2),
	('Array<double> P(50);', 1, 2),
	--Q3
	('It outputs nothing because it is not possible to use char values instead of integer ones', 0, 3),
	('65 65', 0, 3),
	('A A', 0, 3),
	('A 65', 1, 3),
	('65 A', 0, 3);

INSERT INTO ExamAttempt
	([Terminated], [Started], [StartedAt], [UserID], [ExamID])
VALUES
	(0, 0, NULL, 1, 1);
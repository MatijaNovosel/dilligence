INSERT INTO Exam
	(Naziv, SubjectID)
VALUES
	('General programming exam', 147);

INSERT INTO Question
	(Title, Content, ExamID, TypeID)
VALUES
	(
		'Javascript',
		'What is the output of this block of code \n var numbers = [ 1, 2, 3, 4, 5 ];\nnumbers.sort((a, b) => { \n  if(a > b) return 1; \n  else return -1;\n});\nconsole.log(numbers);',
		1,
		1
),
	(
		'C++ 1',
		'How would One initialize an instance of this generic class?\n template <class T>\nclass Array {\npublic:\n\tT* array;\n\tint NumberOfElements;\n\tArray(int n);\n\t~Array() { delete[] array; }\n}\n\ntemplate <class T>\nArray<T>::Array(int n) {\n\tNumberOfElements = n;\n\tarray = new T[n];\n}',
		1,
		1
),
	(
		'C++ 2',
		'What is the output of this block of code?\n template\nclass A\n{\nprotected:\n\tT x;\npublic:\n\tA(T x) { this->x = x; }\n\tvoid print()\n\t{\n\t\tcout << x << " ";\n\t}\n};\n\nint main()\n{\n\tA a("A");\n\tA b("A");\n\ta.print(); b.print();\n}',
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
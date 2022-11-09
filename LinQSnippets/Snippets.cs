using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinQSnippets
{
	public class Snippets
	{
		static public void BasicLinQ()
		{
			string[] cars = {
				"VW Golf",
				"VW California",
				"Audi A3",
				"Audi A5",
				"Fiat Punto",
				"Seat Ibiza",
				"Seat León"
			};

			//SELECT * of Cars
			var carList = from car in cars select car;

			foreach (var car in carList)
			{
				Console.WriteLine(car);
			}

			//SELECT where car is Audi
			var audiList = from car in cars where car.Contains("Audi") select car;
			foreach (var car in audiList)
			{
				Console.WriteLine(car);
			}
		}
		//Number Examples
		static public void LinqNumbers()
		{
			List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			//Each number * 3
			//Take all numbers but 9
			//Sort ascending

			var processedNumberList =
				numbers
					.Select(num => num * 3)
					.Where(num => num != 9)
					.OrderBy(num => num);
			foreach (var number in processedNumberList)
			{
				Console.Write(number + ", ");
			}
		}
		static public void SearchExamples()
		{
			List<string> textList = new List<string>
			{
				"a",
				"bx",
				"c",
				"d",
				"e",
				"cj",
				"f",
				"c"
			};
			//Find first element
			var first = textList.First();
			//Fist element that is "C"
			var cText = textList.First(tx => tx.Equals("c"));
			//first element that contains "J"
			var jText = textList.First(tx => tx.Contains("j"));
			//first element that contains "j" or present default value
			var jOrDefaultText = textList.FirstOrDefault(tx => tx.Contains("j"));
			//last element that contains "tz" or present default value
			var zOrDefaultText = textList.LastOrDefault(tx => tx.Contains("z"));
			//UNique value
			var uniqueValueText = textList.Single();
			var uniqueOrDefaultText = textList.SingleOrDefault();

			int[] evenNumbers = { 0, 2, 4, 6, 8 };
			int[] otherEvenNumbers = { 0, 2, 6 };

			//Obtein 4 and 8
			var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
		}
		static public void MultipleSelects()
		{
			//Select many
			string[] myOpinions =
				{
					"opinion 1, text 1",
					"opinion 2, text 2",
					"opinion 3, text 3"

				};
			var opinionSelection = myOpinions.SelectMany(tx => tx.Split(", "));

			var enterprises = new[]
			{
				new Enterprise()
				{
					Id = 1,
					Name = "Enterprise 1",
					Employees = new[]
					{
						new Employee()
						{
							Id = 1,
							Name = "Pepe",
							Email = "pepe@prueba.com",
							Salary = 3000
						},
						new Employee()
						{
							Id = 2,
							Name = "Juan",
							Email = "juan@prueba.com",
							Salary = 2000
						},
						new Employee()
						{
							Id = 3,
							Name = "Mario",
							Email = "mario@prueba.com",
							Salary = 1500
						},
						new Employee()
						{
							Id = 4,
							Name = "Jose",
							Email = "jose@prueba.com",
							Salary = 500
						}
					}
				},
				new Enterprise()
				{
					Id = 2,
					Name = "Enterprise 2",
					Employees = new[]
					{
						new Employee()
						{
							Id = 5,
							Name = "Martin",
							Email = "martin@prueba.com",
							Salary = 3000
						},
						new Employee()
						{
							Id = 6,
							Name = "Ana",
							Email = "ana@prueba.com",
							Salary = 2000
						},
						new Employee()
						{
							Id = 7,
							Name = "Rodolfo",
							Email = "rodolfo@prueba.com",
							Salary = 1500
						},
						new Employee()
						{
							Id = 8,
							Name = "Marta",
							Email = "marta@prueba.com",
							Salary = 500
						}
					}
				}
			};
			//Obtener todos los objetos (Empleados) de todas las empresas

			var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

			//Saber si cualquier lista está vacía

			bool hasEnterprises = enterprises.Any();
			bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

			//Todas las empresas con empleados con al menos 1000€ de salario
			bool hasEnterprisesWithEmployeesWithAtLeastOrEqual1000Salary =
				enterprises.Any(enterprise => enterprise.Employees
				.Where(employee => employee.Salary >= 1000).Any());


		}

		static public void LinqCollections()
		{
			var firstList = new List<string> { "a", "b", "c" };
			var secondList = new List<string> { "a", "c", "d" };

			//INNER JOIN
			var commonResult = from element in firstList
							   join secondElement in secondList
							   on element equals secondElement
							   select new { element, secondElement };

			var commonResult2 = firstList.Join(secondList
								, element => element
								, secondElement => secondElement
								, (element, secondElement) => new { element, secondElement }
								);

			//OUTER JOIN - LEFT
			var leftOuterJoin = from element in firstList
								join secondElement in secondList
								on element equals secondElement
								into temporalList
								from temporalElement in temporalList.DefaultIfEmpty()
								where element != temporalElement
								select new { Element = element };
			var leftOuterJoin2 = from element in firstList
								 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
								 select new { Element = element, SecondElement = secondElement };


			var rightOuterJoin = from secondElement in secondList
								 join element in firstList
								 on secondElement equals element
								 into temporalList
								 from temporalElement in temporalList.DefaultIfEmpty()
								 where secondElement != temporalElement
								 select new { Element = secondElement };
			//UNION
			var unionList = leftOuterJoin.Union(rightOuterJoin);
		}
		static public void SkipTakeLinq()
		{
			var myList = new[]
			{
				1,2,3,4,5,6,7,8,9
			};
			//SKIP
			var skipFirstTwoValues = myList.Skip(2); //{3,4,5,6,7,8,9}
			var skipLastTwoValues = myList.SkipLast(2); //{1,2,3,4,5,6,7}
			var skipWhileSmallerThanFour = myList.SkipWhile(num => num < 4); //{4,5,6,7,8,9}
																			 //TAKE
			var takeFirstTwoValues = myList.Take(2); //{1,2}
													 //lo demás igual que skip

		}

		// Paging

		static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
		{
			int startIndex = (pageNumber - 1) * resultsPerPage;
			return collection.Skip(startIndex).Take(resultsPerPage);
		}

		static public void LinqVariables()
		{
			int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			var aboveAverage = from number in numbers
							   let average = numbers.Average()
							   let nSquare = Math.Pow(number, 2)
							   where nSquare > average
							   select number;

			Console.WriteLine("Average: {0}", numbers.Average());
			
			foreach(int number in aboveAverage)
			{
				Console.WriteLine("Number: {0} => Square: {1}", number, Math.Pow(number, 2));
			}
		}

		// ZIP de LinQ
		static public void ZipLinq()
		{
			int[] numbers = { 1, 2, 3, 4, 5 };
			string[] stringNumbers = { "one", "two", "three", "four", "five" };

			IEnumerable<string> zipNumbers = numbers.Zip(
				stringNumbers, (number, word) => number + " = " + word);

			// { "1 = one", "2 = two", ....}
		}

		//Repeat & Range
		static public void RepeatRangeLinq()
		{
			//Generate collection from 1 to 1000
			var first1000 = Enumerable.Range(1, 1000);

			//Repeat a value n times
			var fiveXs = Enumerable.Repeat("X", 5);
		}

		static public void StudentsLinQ()
		{
			var classRoom = new[]
			{
				new Student
				{
					Id = 1,
					Name = "Pepe",
					Grade = 90,
					Certified = true
				},
				new Student
				{
					Id = 2,
					Name = "Manolo",
					Grade = 50,
					Certified = false
				},
				new Student
				{
					Id = 3,
					Name = "Jaime",
					Grade = 96,
					Certified = true
				},
				new Student
				{
					Id = 4,
					Name = "María",
					Grade = 10,
					Certified = false
				},
				new Student
				{
					Id = 5,
					Name = "Pedro",
					Grade = 50,
					Certified = true
				}

			};

			var certifiedStudents = from student in classRoom
									where student.Certified
									select student;
			var notCertifiedStudents = from student in classRoom
									   where !student.Certified//student.Certified == false
									   select student;
			var aprovedStudents = from student in classRoom
								  where student.Grade >= 50 && student.Certified
								  select student.Name;
		}

		//ALL 
		static public void AllLinQ()
		{
			var numbers = new List<int>() { 1, 2, 3, 4, 5};

			bool AllAreSmallerThan10 = numbers.All(x => x > 10); // true

			bool AllAreBiggerThan2 = numbers.All(x => x >= 2); // false

			var emptyList = new List<int>();
			bool AllNumbersAreGreaterThan0 = numbers.All(x => x > 0); //true
		}

		//Aggregate
		static public void AggregateQueries()
		{
			int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			// sum all numbers
			int sum =numbers.Aggregate((prevSum, current) => prevSum + current);
			//0, 1 = 1
			//1, 2 = 3
			//3, 3 = 6
			// ....
			//45, 10 = 55

			string[] words = { "hello", ", ", "my ", "name ", "is ", "Martin" };

			string greetings = words.Aggregate((prevWord, current) => prevWord + current);

		}

		//Distinct

		static public void distinctValues()
		{
			int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
			IEnumerable<int> distinctValues = numbers.Distinct(); //admite sobrecargas para incluir funciones
		}

		//GroupBy
		static public void GroupByExamples()
		{
			List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 10 };

			// Obtein only even numbers and generate two groups
			var grouped = numbers.GroupBy(x => x % 2 == 0);
			foreach (var group in grouped)
			{
				foreach (var value in group)
				{
					Console.WriteLine(value); //1,3,5,7,9 .....2,4,6,8,10
											  //primero los que no lo cumplen
											  // despues los que sí
				}
			}
			// Another example
			var classRoom = new[]
			{
				new Student
				{
					Id = 1,
					Name = "Pepe",
					Grade = 90,
					Certified = true
				},
				new Student
				{
					Id = 2,
					Name = "Manolo",
					Grade = 50,
					Certified = false
				},
				new Student
				{
					Id = 3,
					Name = "Jaime",
					Grade = 96,
					Certified = true
				},
				new Student
				{
					Id = 4,
					Name = "María",
					Grade = 10,
					Certified = false
				},
				new Student
				{
					Id = 5,
					Name = "Pedro",
					Grade = 50,
					Certified = true
				}

			};
			var certifiedStudents = classRoom.GroupBy(classRoom => classRoom.Certified);
			//We obtain two groups
			// first: not certified
			// second: certified students
			foreach (var group in certifiedStudents)
			{
				Console.WriteLine("------{0}------", group.Key);
				foreach (var student in group)
				{
					Console.WriteLine(student.Name);//primero los que no lo cumplen
											  // despues los que sí
				}
			}
		}

		static public void RelationsLinq()
		{
			List<Post> posts = new List<Post>()
			{
				new Post
				{
					Id=1,
					Title="My first Post",
					Content="Nothing Here",
					Created=DateTime.Now,
					Comments= new List<Comment>()
					{
						new Comment
						{
							Id=1,
							Title="Your Comment",
							Content="I don't like it",
							Created=DateTime.Now
						},
						new Comment
						{
							Id=2,
							Title="Your Comment",
							Content="I don't like it",
							Created=DateTime.Now
						},
						new Comment
						{
							Id=3,
							Title="Your Comment",
							Content="I don't like it",
							Created=DateTime.Now
						}
					}

				},
				new Post
				{
					Id=2,
					Title="My first Post",
					Content="Nothing Here",
					Created=DateTime.Now,
					Comments= new List<Comment>()
					{
						new Comment
						{
							Id=4,
							Title="Your Comment",
							Content="I don't like it",
							Created=DateTime.Now
						},
						new Comment
						{
							Id=5,
							Title="Your Comment",
							Content="I don't like it",
							Created=DateTime.Now
						},
						new Comment
						{
							Id=6,
							Title="Your Comment",
							Content="I don't like it",
							Created=DateTime.Now
						}
					}

				}

			};
			var commentsContent = posts.SelectMany(posts => posts.Comments
										,(post, comment) => new {PostId = post.Id
																,CommentId = comment.Id});
				
		}


	}


		


	}


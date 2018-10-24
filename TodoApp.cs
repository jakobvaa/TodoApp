using System;
using System.Collections.Generic;
using System.IO;
namespace dips {
    public class TodoApp {
        private List<TodoItem> todoList = new List<TodoItem> ();
        private string path = System.AppDomain.CurrentDomain.BaseDirectory;
        private int idCounter = 0;

        public TodoApp () {
            ReadTodos ();
        }

        //Reads the file with todoItems
        private void ReadTodos () {
            try {
                string[] lines = System.IO.File.ReadAllLines (@path + "todos.txt");
                foreach (string line in lines) {
                    Add (line);
                }
            }
            //catch file not found exception, IO
            catch (ArgumentException e) {
                Console.WriteLine ("There was no data saved");
                Console.WriteLine (e);
            } catch (Exception e) {
                Console.WriteLine (e);
            }
        }

        //If todoList is not empty, write all todos to todos.txt
        // so we can save it between runs.todoList
        private void SaveTodos () {
            if (todoList.Count != 0) {
                string[] list = ListToArray (todoList);
                try {
                    System.IO.File.WriteAllLines (@path + "todos.txt", list);
                } catch (IOException e) {
                    Console.WriteLine (e);
                }
            }
        }
        //Converts the List<TodoItem> to an array of "manageable" strings
        //Assumes that the id of the todos is not important between the runs.
        private string[] ListToArray (List<TodoItem> todos) {
            string[] array = new string[todos.Count];
            for (int i = 0; i < todos.Count; i++) {
                array[i] = "" + todos[i].GetText ();
            }
            return array;
        }
        public void Add (String todo) {
            this.todoList.Add (new TodoItem (idCounter, todo));
            idCounter++;
        }

        public void Do (int id) {
            for (int i = 0; i < this.todoList.Count; i++) {
                if (id == todoList[i].GetID ()) {
                    todoList.RemoveAt (i);
                    Console.WriteLine ("Removed item: " + id);
                    return;
                }
            }
            Console.WriteLine ("Item does not exist");

        }
        public void Print () {
            for (int i = 0; i < todoList.Count; i++) {
                Console.WriteLine (this.todoList[i]);
            }
        }
        public static void Main (string[] args) {
            TodoApp app = new TodoApp ();
            Console.WriteLine ("###TodoApp###");

            string answer;

            do {
                Console.WriteLine ("");
                Console.WriteLine ("Complete a Todo: DO #");
                Console.WriteLine ("Add a Todo: ADD 'Task to be done'");
                Console.WriteLine ("Print all Todos: PRINT");
                Console.WriteLine ("To exit: EXIT");
                Console.WriteLine ("");
                answer = Console.ReadLine ();
                string[] array;
                //Makes sure the input is valid
                if (answer.Length != 0) {
                    array = answer.Split (" ");
                    if (array.Length == 1 && !array[0].Equals ("PRINT") && !array[0].Equals ("EXIT")) {
                        Console.WriteLine ("Please enter a valid input");
                        break;
                    }
                } else {
                    break;
                }
                string action = array[0];
                switch (action) {
                    case "DO":
                        int id;
                        if (int.TryParse (array[1], out id)) {
                            app.Do (int.Parse (array[1]));
                        }
                        break;

                    case "ADD":
                        string task = answer.Remove (0, 4);
                        app.Add (task);
                        break;

                    case "PRINT":
                        app.Print ();
                        break;

                    case "EXIT":
                        break;

                    default:
                        Console.WriteLine ("Please enter a valid input");
                        break;
                }

            } while (answer != "EXIT");
            app.SaveTodos ();
        }
    }
}
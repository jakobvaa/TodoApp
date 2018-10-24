using System;
namespace dips
{
    public class TodoItem {
        private int id;
        private string text;

        public TodoItem (int id, string todo) {
            this.id = id;
            this.text = todo;
        }

        public int GetID () {
            return this.id;
        }
        public String GetText () {
            return this.text;
        }

        public override string ToString () {
            return "#" + this.id + " " + this.text;
        }
    }
}

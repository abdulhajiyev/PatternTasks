using System;

namespace MediatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Button button = new ();
            Textbox textbox = new ();
            Checkbox checkbox = new ();
            new AuthenticationDialog(button, textbox, checkbox);

            Console.WriteLine("Client triggets operation Button.");
            button.Click();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation Textbox.");
            textbox.Textchanged();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation Checkbox.");
            checkbox.Checked();

        }
    }

    public interface IMediator
    {
        void Notify(object sender, string ev);
    }


    // Concrete mediator
    class AuthenticationDialog : IMediator
    {
        private readonly Button _button;
        private readonly Textbox _textbox;
        private readonly Checkbox _checkbox;

        public AuthenticationDialog(Button button, Textbox textbox, Checkbox checkbox)
        {
            _button = button;
            _button.SetMediator(this);
            _textbox = textbox;
            _textbox.SetMediator(this);
            _checkbox = checkbox;
            _checkbox.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            switch (ev)
            {
                case "Click":
                    Console.WriteLine("Mediator reacts on Button and triggers folowing operations:");
                    _button.Click();
                    break;
                case "Textbox":
                    Console.WriteLine("Mediator reacts on Textbox and triggers following operations:");
                    _textbox.Textchanged();
                    break;
                case "Checkbox":
                    Console.WriteLine("Mediator reacts on Checkbox and triggers following operations:");
                    _checkbox.Checked();
                    break;
            }
        }
    }


    class BaseComponent
    {
        protected IMediator Mediator;

        protected BaseComponent(IMediator mediator = null)
        {
            Mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }

    class Button : BaseComponent
    {
        public void Click()
        {
            Console.WriteLine("Button clicked");
            Mediator.Notify(this, "click");
        }
    }

    class Textbox : BaseComponent
    {
        public void Textchanged()
        {
            Console.WriteLine("Txt changed");
            Mediator.Notify(this, "text");
        }
    }

    class Checkbox : BaseComponent 
    {
        public void Checked()
        {
            Console.WriteLine("Cbx checked");
            Mediator.Notify(this, "checked");
        }
    }
}

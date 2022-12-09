using UnityEngine.Events;


namespace Project.Sender
{
    public class ProduceButtonMessageSender : ButtonMessageSender
    {
        public static UnityAction OnButtonPressed { get; set; }

        public override void ButtonPressed()
        {
            OnButtonPressed?.Invoke();
        }
    }

}

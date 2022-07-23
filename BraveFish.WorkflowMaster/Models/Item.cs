namespace BraveFish.WorkflowMaster.Models
{
    public class Item
    {
        public string FromState { get; set; }

        public string StateName { get; set; }

        public List<ItemAction> ItemActions { get; set; }
    }
}

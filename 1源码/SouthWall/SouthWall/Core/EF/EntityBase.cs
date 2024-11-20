namespace SouthWall
{
    public class EntityBase
    {
        public DateTime? F_CreateTime { get; set; }
        public DateTime? F_UpdateTime { get; set; }

        public virtual void InitCreate()
        {            
            this.F_CreateTime = DateTime.Now;
        }

        public void InitUpdate()
        {
            this.F_UpdateTime = DateTime.Now;
        }
    }
}

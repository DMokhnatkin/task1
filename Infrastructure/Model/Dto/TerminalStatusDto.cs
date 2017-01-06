namespace Infrastructure.Model.Dto
{
    public class TerminalStatusDto
    {
        public string TerminalId { get; set; }

        public MeteringDto LastMetering { get; set; }
    }
}

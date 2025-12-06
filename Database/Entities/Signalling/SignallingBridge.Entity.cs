using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMPIRIAN.Database.Signalling;

public class SignallingBridge
{
    public SignallingBridge() { }
    public SignallingBridge(Guid initiatorUserID)
    {
        InitiatorUserID = initiatorUserID;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public Guid InitiatorUserID { get; set; }
    public virtual Users.User? InitiatorUser { get; set; }
}
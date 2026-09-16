using Quack.Messages;
using System.Text;

namespace Quack.Tests;

[TestClass]
public class Stage1_SerializationTests
{
    #region Binary JoinMessage

    [TestMethod]
    public void JoinMessage_Serialize_LayoutIsCorrect()
    {
        var message = new JoinMessage { Name = "Duck" };
        byte[] buffer = new byte[message.GetSize()];
        message.Serialize(buffer);

        // Header: 4 bytes length (4)
        Assert.AreEqual(4, BitConverter.ToInt32(buffer, 0), "The first 4 bytes should contain the length of the string 'Duck' (which is 4 bytes).");
        // Payload: "Duck"
        Assert.AreEqual("Duck", Encoding.UTF8.GetString(buffer, 4, 4), "Bytes 4-8 should contain the UTF8 encoded string 'Duck'.");
    }

    [TestMethod]
    public void JoinMessage_Deserialize_ParsedCorrectly()
    {
        // Layout: [Len: 5] [Q u a c k]
        byte[] raw = [0x05, 0x00, 0x00, 0x00, 0x51, 0x75, 0x61, 0x63, 0x6B];
        
        var result = (JoinMessage)JoinMessage.Deserialize(raw);

        Assert.AreEqual("Quack", result.Name, "Deserialized 'Name' property should match the string 'Quack' from the byte array.");
    }

    #endregion

    #region Binary InputMessage

    [TestMethod]
    public void InputMessage_Serialize_BitsAreCorrect()
    {
        // Bit 0: Up (1), Bit 3: Right (8), Bit 4: Sprint (16) = 25
        var message = new InputMessage { Up = true, Right = true, Sprint = true };
        byte[] buffer = new byte[1];
        message.Serialize(buffer);

        Assert.AreEqual(25, buffer[0], "The byte should equal 25 (1+8+16) representing flags Up|Right|Sprint.");
    }

    [TestMethod]
    public void InputMessage_Deserialize_BitsParsedCorrectly()
    {
        // Input: Down (2) + Left (4) = 6
        byte[] raw = [0x06];

        var result = (InputMessage)InputMessage.Deserialize(raw);

        Assert.IsTrue(result.Down, "Bit 1 (value 2) was set, so 'Down' should be true.");
        Assert.IsTrue(result.Left, "Bit 2 (value 4) was set, so 'Left' should be true.");
        Assert.IsFalse(result.Up, "Bit 0 (value 1) was NOT set, so 'Up' should be false.");
        Assert.IsFalse(result.Right, "Bit 3 (value 8) was NOT set, so 'Right' should be false.");
        Assert.IsFalse(result.Sprint, "Bit 4 (value 16) was NOT set, so 'Sprint' should be false.");
    }

    #endregion

    #region JSON Messages

    [TestMethod]
    public void WelcomeMessage_RoundTrip_Works()
    {
        var original = new WelcomeMessage { PlayerId = 42, GameTime = DateTime.UtcNow };
        string json = original.Serialize();
        
        Assert.IsFalse(string.IsNullOrWhiteSpace(json), "Serialized JSON should not be empty or whitespace.");
        
        var result = (WelcomeMessage?)IJsonMessage.Deserialize(MessageType.Welcome, json);
        
        Assert.IsNotNull(result, "Deserializing the JSON back to WelcomeMessage returned null.");
        Assert.AreEqual(original.PlayerId, result.PlayerId, "Round-tripped 'PlayerId' property does not match.");
        // Compare ticks to avoid precision issues with string serialization of dates
        Assert.AreEqual(original.GameTime.Ticks / 10000, result.GameTime.Ticks / 10000, 10, "GameTime should match (within precision limits).");
    }

    #endregion
}

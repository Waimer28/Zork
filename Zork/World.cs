using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Zork
{
    public class World
    {
        public HashSet<Room> Rooms { get; set; }

        [JsonIgnore]
        public IReadOnlyDictionary<string, Room> RoomsByName => mRoomsByName;
        public Player SpawnPlayer() => new Player(this, StartingLocation);

        [OnDeserialized]
        private void OnDeseralized(StreamingContext context)
        {
            mRoomsByName = Rooms.ToDictionary(room => room.Name, room => room);
            foreach (Room room in Rooms)
            {
                room.UpdateNeighbors(this);
            }
        }
    [JsonProperty]
    private string StartingLocation { get; set; }
    private Dictionary<string, Room> mRoomsByName;
    }
}

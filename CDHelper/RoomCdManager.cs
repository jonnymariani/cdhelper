using CDHelper.Models;

namespace CDHelper
{
    public static class RoomCdManager
    {
        private static readonly List<RoomCdData> _rooms = new();

        // Guarda o ID da sala atual / Stores the current room ID
        public static int? CurrentRoomId { get; private set; }

        /// <summary>
        /// Cria uma nova sala (se ainda não existir) e define como sala atual.
        /// Creates a new room (if not exists) and sets it as the current room.
        /// </summary>
        public static void CreateRoom(int roomId)
        {
            var existingRoom = _rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (existingRoom == null)
            {
                _rooms.Add(new RoomCdData(roomId));
            }
            CurrentRoomId = roomId;
        }

        /// <summary>
        /// Adiciona uma lista de CDs a uma sala existente.
        /// Adds a list of CDs to an existing room.
        /// </summary>
        public static void AddCdsToRoom(int roomId, IEnumerable<CdData> cds)
        {
            CreateRoom(roomId);

            var room = _rooms.FirstOrDefault(r => r.RoomId == roomId);

            room!.CDS = cds;

        }

        public static void AddCdsToRoom(IEnumerable<CdData> cds)
        {
            AddCdsToRoom(GetCurrentRoomId() ?? 0, cds);
        }

        /// <summary>
        /// Retorna o RoomId da sala atual.
        /// Returns the current room ID.
        /// </summary>
        public static int? GetCurrentRoomId()
        {
            return CurrentRoomId;
        }

        /// <summary>
        /// Define manualmente o RoomId da sala atual.
        /// Manually sets the current room ID.
        /// </summary>
        public static void SetCurrentRoomId(int roomId)
        {
            CurrentRoomId = roomId;
        }

        /// <summary>
        /// Retorna os dados da sala atual.
        /// Returns the current room data.
        /// </summary>
        public static RoomCdData? GetCurrentRoom()
        {
            if (CurrentRoomId.HasValue)
                return GetRoom(CurrentRoomId.Value);
            return null;
        }

        /// <summary>
        /// Busca uma sala específica pelo ID.
        /// Finds a specific room by its ID.
        /// </summary>
        public static RoomCdData? GetRoom(int roomId)
        {
            return _rooms.FirstOrDefault(r => r.RoomId == roomId);
        }

        /// <summary>
        /// Remove uma sala pelo ID.
        /// Removes a room by its ID.
        /// </summary>
        public static bool RemoveRoom(int roomId)
        {
            var room = _rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room != null)
            {
                _rooms.Remove(room);
                if (CurrentRoomId == roomId)
                    CurrentRoomId = null; // se remover a sala atual, zera / if removing the current room, reset it
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove todas as salas da memória.
        /// Clears all rooms from memory.
        /// </summary>
        public static void ClearAll()
        {
            _rooms.Clear();
            CurrentRoomId = null;
        }

        /// <summary>
        /// Retorna todas as salas salvas.
        /// Returns all saved rooms.
        /// </summary>
        public static IEnumerable<RoomCdData> GetAllRooms()
        {
            return _rooms;
        }

        public static IEnumerable<CdData>? GetCurrentRoomCds()
        {
            var room = GetCurrentRoom();
            return room?.CDS;
        }
    }
}

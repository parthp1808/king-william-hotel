CREATE PROCEDURE [dbo].[stpRooms]
	@start varchar,
	@end varchar
AS
	BEGIN
	select tblRooms.RoomID, tblRooms.RoomFloor,tblRoomStatuses.StatusDescription from tblRooms inner join tblRoomStatuses on tblRoomStatuses.StatusID = tblRooms.StatusID  where tblRooms.RoomID not in (select RoomID from  tblReservations where ReservationStartDate < @start and ReservationEndDate > @end)
	END
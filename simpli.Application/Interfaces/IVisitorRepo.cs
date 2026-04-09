using simpli.Domain.Entities;

interface IVisitorRepo
{
    Task<VisitorDto> CheckIn(CheckInDto dto, int companyId, int roomId);
    Task CheckOut(CheckOutDto dto);
}
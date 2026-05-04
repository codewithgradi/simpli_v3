using simpli.Domain.Entities;

public interface IVisitorRepo
{
    Task<VisitorDto> CheckIn(CheckInDto dto, int companyId, int roomId);
    Task CheckOut(CheckOutDto dto, int id);
    Task<VisitorDto> GetVisitor(int id);
}
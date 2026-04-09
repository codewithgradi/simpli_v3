using simpli.Domain.Entities;

public class VisitorRepo : IVisitorRepo
{
    public Task<VisitorDto> CheckIn(CheckInDto dto, int companyId, int roomId)
    {
        throw new NotImplementedException();
    }

    public Task CheckOut(CheckOutDto dto)
    {
        throw new NotImplementedException();
    }
}
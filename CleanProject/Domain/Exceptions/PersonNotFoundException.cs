using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class PersonNotFoundException(Guid personId)
    : NotFoundException($"The webinar with the identifier {personId} was not found.");
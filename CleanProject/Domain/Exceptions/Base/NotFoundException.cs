namespace Domain.Exceptions.Base;

public abstract class NotFoundException(string personId) : Exception;
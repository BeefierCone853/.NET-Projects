﻿using Application.Exceptions;
using Domain.Shared;

namespace WebApi.FunctionalTests.Contracts;

internal sealed class CustomProblemDetails
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public List<ValidationError> Errors { get; set; }
}
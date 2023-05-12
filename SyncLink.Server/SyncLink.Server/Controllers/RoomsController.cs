using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Server.Controllers.Base;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoomsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
}
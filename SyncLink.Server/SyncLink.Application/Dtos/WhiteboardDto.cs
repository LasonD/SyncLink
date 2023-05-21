namespace SyncLink.Application.Dtos;

public class WhiteboardDto
{
    public float Id { get; set; }

    public string Name { get; set; } = null!;

    public List<WhiteboardElementDto> WhiteboardElements { get; set; } = null!;

    public float OwnerId { get; set; }

    public float GroupId { get; set; }

    public DateTime LastUpdatedTime { get; set; }
}

public class WhiteboardElementDto
{
    public string Type { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Id { get; set; } = null!;
    public float X { get; set; }
    public float Y { get; set; }
    public float Rotation { get; set; }
    public float Opacity { get; set; }
    public WhiteboardElementOptionsDto Options { get; set; } = null!;
}

public class WhiteboardElementOptionsDto
{
    public float? Width { get; set; }
    public float? Height { get; set; }
    public float? StrokeWidth { get; set; }
    public string? StrokeColor { get; set; }
    public string? Fill { get; set; }
    public string LineJoin { get; set; } = null!;
    public string LineCap { get; set; } = null!;
    public float? Left { get; set; }
    public float? Top { get; set; }
    public float? FontSize { get; set; }
    public string FontFamily { get; set; } = null!;
    public string? FontStyle { get; set; }
    public string? FontWeight { get; set; }
    public string? Color { get; set; }
    public string? DashArray { get; set; }
    public float? DashOffset { get; set; }
    public float? X1 { get; set; }
    public float? Y1 { get; set; }
    public float? X2 { get; set; }
    public float? Y2 { get; set; }
    public float? Rx { get; set; }
    public float? Ry { get; set; }
    public float? Cx { get; set; }
    public float? Cy { get; set; }
}
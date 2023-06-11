using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups;

namespace SyncLink.Application.Domain.Features;

public class Whiteboard : EntityBase
{
    public string Name { get; set; } = null!;

    public List<WhiteboardElement> WhiteboardElements { get; set; } = new();

    public int? OwnerId { get; set; }

    public User Owner { get; set; } = null!;

    public int? GroupId { get; set; }
    
    public Group Group { get; set; } = null!;

    public DateTime LastUpdatedTime { get; set; }
}

public class WhiteboardElement
{
    public ElementTypeEnum Type { get; set; }
    public string Value { get; set; } = null!;
    public string Id { get; set; } = null!;
    public float X { get; set; }
    public float Y { get; set; }
    public float Rotation { get; set; }
    public float Opacity { get; set; }
    public WhiteboardElementOptions Options { get; set; } = null!;

    public User Author { get; set; } = null!;
    public int? AuthorId { get; set; }
}

public enum ElementTypeEnum
{
    BRUSH,
    LINE,
    RECT,
    ELLIPSE,
    IMAGE,
    TEXT,
    SHAPE
}

public class WhiteboardElementOptions
{
    public float? Width { get; set; }
    public float? Height { get; set; }
    public float? StrokeWidth { get; set; }
    public string? StrokeColor { get; set; }
    public string? Fill { get; set; }
    public LineJoinEnum LineJoin { get; set; }
    public LineCapEnum LineCap { get; set; }
    public float? Left { get; set; }
    public float? Top { get; set; }
    public float? FontSize { get; set; }
    public string? FontFamily { get; set; }
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

public enum LineCapEnum
{
    BUTT,
    SQUARE,
    ROUND
}

public enum LineJoinEnum
{
    MITER,
    ROUND,
    BEVEL,
    MITER_CLIP
}


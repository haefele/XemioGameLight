using System.Drawing;

namespace Xemio.GameLight.Entities
{
    public static class EntityHelper
    {
        public static Intersection WouldIntersectWith(Entity first, PointF newLocation, Entity second)
        {
            var newRectangle = new RectangleF(newLocation, first.Size);

            if (newRectangle.IntersectsWith(second.Rectangle) == false)
                return Intersection.None;

            newRectangle.Intersect(second.Rectangle);

            if (newRectangle.X > newLocation.X)
                return Intersection.Right;
            if (newRectangle.Y > newLocation.Y)
                return Intersection.Bottom;
            if (newRectangle.X + newRectangle.Width < newLocation.X + first.Size.Width)
                return Intersection.Left;
            if (newRectangle.Y + newRectangle.Height < newLocation.Y + first.Size.Height)
                return Intersection.Top;

            return Intersection.None;
        }
    }
}
namespace Shapes
{
    internal class ShapeProgram
    {
        static void Main(string[] args)
        {
            // For 2D shape cases
            List<Shape> shapes = new List<Shape>{
                new Circle(5),
                new Rectangle(2, 5),
                new Triangle(3, 7)
            };

            foreach (Shape shape in shapes)
            {
                Console.WriteLine(shape.GetType().Name + " area = " + shape.CalculateArea());
            }
            
            // For 3D shape cases
            List<Shape3D> shapes3d = new List<Shape3D>();

            foreach (Shape shape in shapes)
            {
                if (shape is Circle circle)
                {
                    shapes3d.Add(new Sphere(circle));
                    shapes3d.Add(new Cylinder(circle, 5));
                    shapes3d.Add(new Cone(circle, 5));
                }
                else if (shape is Triangle triangle)
                {

                    shapes3d.Add(new Prism(triangle, 5));
                    shapes3d.Add(new Pyramid(triangle, 5));
                }
            }
            foreach (Shape3D shape3d in shapes3d)
            {
                Console.WriteLine(shape3d.GetType().Name + " with " +
                shape3d.BaseShape.GetType().Name + " base | Volume = " +
                shape3d.CalculateVolume());
            }
        }
        
        // ------------------- 2D Shapes -------------------------------------------------
        // Creating abstract base Shape class
        public abstract class Shape 
        {
            public abstract double CalculateArea(); // abstract methods have no body {}
        }

        // Creating 3 subclasses (Circle, Rectangle, Triangle) from Shape base class
        public class Circle : Shape 
        {
            public double Radius { get; set; }
            public Circle(double radius) 
            {
                Radius = radius;
            }
            public override double CalculateArea()
            {
                return Math.PI * Radius * Radius;
            }
        }
        public class Rectangle : Shape 
        {
            public double Length { get; set; }
            public double Width { get; set; }

            public Rectangle(double length, double width) 
            {
                Length = length;
                Width = width;
            }
            public override double CalculateArea()
            {
                return Length * Width;
            }
        }
        public class Triangle : Shape
        { 
            public double BaseLength { get; set; }
            public double Height { get; set; }

            public Triangle(double baseLength, double height) 
            {
                BaseLength = baseLength;
                Height = height;
            }
            public override double CalculateArea()
            {
                return 0.5 * BaseLength * Height;
            }
        }

        // ------------------- 3D Shapes -------------------------------------------------
        // Creating a new abstract base Shape3D class
        public abstract class Shape3D 
        { 
            public Shape BaseShape { get; set; }

            public abstract double CalculateVolume();

            public Shape3D(Shape baseShape)
            {
                BaseShape = baseShape;
            }
        }
        // Creating 5 subclasses (Cylinder, Cone, Sphere, Pyramid, Prism) of Shape3D base class 
        public class Cylinder : Shape3D 
        { 
            public double Height { get; set; }
            public Cylinder(Circle shape, double height) : base(shape)
            {
                Height = height;
            }

            public override double CalculateVolume()
            {
                Circle circle = (Circle)BaseShape;
                return circle.CalculateArea() * Height;
            }
        }
        public class Cone : Shape3D 
        {
            public double Height { get; set; }
            public Cone(Circle shape, double height) : base(shape)
            {
                Height = height;
            }
            public override double CalculateVolume()
            {
                Circle circle = (Circle)BaseShape;
                return (1.0 / 3.0) * circle.CalculateArea() * Height;
            }
        }
        public class Sphere : Shape3D 
        {
            public Sphere(Circle shape) : base(shape) { }
            public override double CalculateVolume()
            {
                Circle circle = (Circle)BaseShape;
                double radius = circle.Radius;
                return (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3);
            }
        }
        public class Pyramid : Shape3D 
        { 
            public double Height { get; set; }
            public Pyramid(Triangle shape, double height) : base(shape)
            {
                Height = height;
            }
            public override double CalculateVolume()
            {
                Triangle triangle = (Triangle)BaseShape;
                return (1.0 / 3.0) * triangle.CalculateArea() * Height;
            }

        }
        public class Prism : Shape3D 
        {
            public double Height { get; set; }
            public Prism(Triangle shape, double height) : base(shape)
            {
                Height = height;
            }

            public override double CalculateVolume()
            {
                Triangle triangle = (Triangle)BaseShape;
                return triangle.CalculateArea() * Height;
            }
        }
    }
}

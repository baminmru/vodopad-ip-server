using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmExplorer.Spatial;
using OsmExplorer.Units;
using OsmExplorer.Data.Internal;

namespace OsmExplorer.Functions
{
    /// <summary>
    /// Static class containing mathematical methods for working with spatial data.
    /// </summary>
    public static class MathFunctions
    {
        #region Private
        private static double m_Distance(LatLon P1, LatLon P2)
        {
            var dLat = (P1.Lat - P2.Lat) * m_RadiansPerDegree;
            var dLon = (P1.Lon - P2.Lon) * m_RadiansPerDegree;
            var lat1 = P1.Lat * m_RadiansPerDegree;
            var lat2 = P2.Lat * m_RadiansPerDegree;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = earth_radius_meters * c;

            return d;
        }
        private static double m_PDistance(LatLon P1, LatLon P2)
        {
            var x = (P1.Lon - P2.Lon) * m_RadiansPerDegree * Math.Cos((P1.Lat + P2.Lat) * m_RadiansPerHalfDegree);
            var y = (P1.Lat - P2.Lat) * m_RadiansPerDegree;
            var d = Math.Sqrt(x * x + y * y) * earth_radius_meters;

            return d;
        }
        private const double m_RadiansPerDegree = 0.0174532925;
        private const double m_RadiansPerHalfDegree = 0.00872664626;
        #endregion
        /// <summary>
        /// Radius of the earth in meters. This field is constant.
        /// </summary>
        public const double earth_radius_meters = 6371000;

        /// <summary>
        /// Calculates the great circle distance along an ordered array or set of LatLons.
        /// </summary>
        /// <param name="points">An ordered array or set of LatLons.</param>
        /// <returns>A DistanceSpan object representing the distance along the set of LatLons.</returns>
        /// <exception cref=" System.ArgumentOutOfRangeException">Argument must be 2 or more LatLons.</exception>
        public static Length Distance(params LatLon[] points)
        {
            if (points.Count() < 2)
                throw new ArgumentOutOfRangeException("Argument must contain at least 2 points");

            double distance = 0;
            for (int i = 1; i < points.Count(); i++)
                distance += m_Distance(points[i - 1], points[i]);

            return new Length(Convert.ToSingle(distance), LengthUnits.Meters);
        }
        /// <summary>
        /// Calculates the Pythagorean approximated distance along an ordered array or set of LatLons. See remarks.
        /// </summary>
        /// <param name="points">An ordered array or set of LatLons.</param>
        /// <returns>A DistanceSpan object representing the distance along the set of LatLons.</returns>
        /// <exception cref=" System.ArgumentOutOfRangeException">Argument must be 2 or more LatLons.</exception>
        /// /// <remarks>The Pythagorean distance assumes the world is flat. For short-
        /// distance calculations this can be an acceptable approximation and may be
        /// preferred for multiple repeat calculations (slightly less than half as expensive
        /// as DistanceTo method).</remarks>
        public static Length PythagoreanDistance(params LatLon[] points)
        {
            if (points.Count() < 2)
                throw new ArgumentOutOfRangeException("Argument must contain at least 2 points");

            double distance = 0;

            for (int i = 1; i < points.Count(); i++)
                distance += m_PDistance(points[i - 1], points[i]);

            return new Length(Convert.ToSingle(distance), LengthUnits.Meters);
        }
        /// <summary>
        /// Gets an initial heading from one LatLon to another.
        /// </summary>
        /// <param name="P1">First LatLon.</param>
        /// <param name="P2">Second LatLon.</param>
        /// <returns>Initial heading in degrees from P1 to P2.</returns>
        public static double GetHeading(LatLon P1, LatLon P2)
        {
            var y = Math.Sin((P2.Lon - P1.Lon) * Math.PI / 180) * Math.Cos((P2.Lat) * Math.PI / 180);
            var x = Math.Cos(P1.Lat * Math.PI / 180) * Math.Sin(P2.Lat * Math.PI / 180) -
                    Math.Sin(P1.Lat * Math.PI / 180) * Math.Cos(P2.Lat * Math.PI / 180) * Math.Cos((P2.Lon - P1.Lon) * Math.PI / 180);
            var Heading = Math.Atan2(y, x) * 180 / Math.PI;
            return Heading;
        }
        /// <summary>
        /// Calculates the difference in two headings in degrees.
        /// </summary>
        /// <param name="Heading1">First heading in degrees.</param>
        /// <param name="Heading2">Second heading in degrees.</param>
        /// <returns>The difference in headings in degrees.</returns>
        public static double HeadingDiff(double Heading1, double Heading2)
        {
            double num = Heading2 - Heading1;
            if (num < -180.0)
                return (num + 360.0);
            if (num > 180.0)
                return (num - 360.0);
            return num;
        }
        /// <summary>
        /// Linearly interpolates between two LatLons and returns an array of points
        /// equal in size to the specified InterpolationFactor.
        /// </summary>
        /// <param name="P1">First LatLon.</param>
        /// <param name="P2">Second LatLon.</param>
        /// <param name="InterpolationFactor">Number of interpolated points to return.</param>
        /// <returns>An array of interpolated LatLons.</returns>
        public static LatLon[] Interpolate(LatLon P1, LatLon P2, int InterpolationFactor)
        {
            LatLon[] ResultArray = new LatLon[InterpolationFactor + 1];

            double dX = (P2.Lon - P1.Lon) / InterpolationFactor;
            double X = P1.Lon;
            double Y = P1.Lat;

            for (int i = 0; i < InterpolationFactor; i++)
            {
                LatLon ll = new LatLon(Y, X);
                ResultArray[i] = ll;
                X += dX;
                var dY = P1.Lat + ((X - P1.Lon) * (P2.Lat - P1.Lat) / (P2.Lon - P1.Lon));
                Y = dY.Equals(double.NaN) ? Y : dY;
            }
            ResultArray[ResultArray.Count() - 1] = P2;
            return ResultArray;
        }
    }
}

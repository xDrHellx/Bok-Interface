using System;
using System.Collections.Generic;

namespace BokInterface.Calculators {
    /// <summary>
    ///     Calculator for movement-related operations.<br/>
    ///     Applicable to all Boktai games.
    /// </summary>
    class MovementCalculator {

        #region Properties

        protected int _previousPosX,
            _previousPosY,
            _previousPosZ;
        protected readonly List<double> _speedBuffer = [];

        #endregion

        #region Methods

        /// <summary>Get the average speed</summary>
        /// <param name="value">Current speed</param>
        /// <param name="size">Maximum buffer limit</param>
        /// <returns><c>double</c>Average speed</returns>
        public double GetAverageSpeed(double value, int size) {

            // Insert the new value at the end of the list
            _speedBuffer.Add(value);

            // If the buffer limit is reached, remove the first value in the list
            if (_speedBuffer.Count == size) {
                _speedBuffer.RemoveAt(0);
            }

            // Calculate the average speed
            double averageSpeed = 0;
            for (int i = 0; i < _speedBuffer.Count; i++) {
                averageSpeed += _speedBuffer[i] / _speedBuffer.Count;
            }

            return averageSpeed;
        }

        /// <summary>Get the movement speed in 3D</summary>
        /// <param name="positionX">Position X</param>
        /// <param name="positionY">Position Y</param>
        /// <param name="positionZ">Position Z</param>
        /// <returns><c>double</c>3D movement speed</returns>
        public double Get3dMovementSpeed(int positionX, int positionY, int positionZ) {

            // Calculate the current speed per direction & in 3D
            int speedX = positionX - _previousPosX;
            int speedY = positionY - _previousPosY;
            int speedZ = positionZ - _previousPosZ;
            double speed3D = Math.Sqrt(
                Math.Pow(speedX, 2)
                + Math.Pow(speedY, 2)
                + Math.Pow(speedZ, 2)
            );

            // Limit the speed to 255
            if (speed3D > 255) {
                speed3D = 255;
            }

            // Update the previous positions with the ones that were passed
            _previousPosX = positionX;
            _previousPosY = positionY;
            _previousPosZ = positionZ;

            return speed3D;
        }

        /// <summary>Get the movement speed in 2D</summary>
        /// <param name="positionX">Position X</param>
        /// <param name="positionY">Position Y</param>
        /// <returns><c>double</c>2D movement speed</returns>
        public double Get2dMovementSpeed(int positionX, int positionY) {

            // Calculate the current speed per direction & in 2D
            int speedX = positionX - _previousPosX;
            int speedY = positionY - _previousPosY;
            double speed2D = Math.Sqrt(
                Math.Pow(speedX, 2)
                + Math.Pow(speedY, 2)
            );

            // Limit the speed to 255
            if (speed2D > 255) {
                speed2D = 255;
            }

            // Update the previous positions with the ones that were passed
            _previousPosX = positionX;
            _previousPosY = positionY;

            return speed2D;
        }

        #endregion
    }
}
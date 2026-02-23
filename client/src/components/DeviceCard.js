import React from 'react';

function DeviceCard({ device, onClick }) {
  const getDeviceIcon = (deviceType) => {
    switch(deviceType) {
      case 'Energy Meter':
        return '⚡';
      case 'Temperature Monitor':
        return '🌡️';
      case 'Protection Relay':
        return '🛡️';
      case 'VFD Drive':
        return '⚙️';
      default:
        return '📡';
    }
  };

  return (
    <div className="device-card" onClick={onClick}>
      <div className="device-card-header">
        <div>
          <div className="device-name">
            {getDeviceIcon(device.deviceType)} {device.name}
          </div>
          <div className="device-type">{device.deviceType}</div>
        </div>
        <div className={`device-status ${device.status.toLowerCase()}`}>
          {device.status}
        </div>
      </div>
      <div className="device-info">
        <div className="info-item">
          <span className="info-label">Slave ID</span>
          <span className="info-value">{device.slaveId}</span>
        </div>
        <div className="info-item">
          <span className="info-label">Location</span>
          <span className="info-value">{device.location}</span>
        </div>
        <div className="info-item">
          <span className="info-label">Baud Rate</span>
          <span className="info-value">{device.baudRate}</span>
        </div>
        <div className="info-item">
          <span className="info-label">Parity</span>
          <span className="info-value">{device.parity}</span>
        </div>
      </div>
    </div>
  );
}

export default DeviceCard;

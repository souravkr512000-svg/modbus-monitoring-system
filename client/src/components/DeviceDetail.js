import React, { useState, useEffect } from 'react';
import axios from 'axios';
import io from 'socket.io-client';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:3001';
const socket = io(API_URL);

function DeviceDetail({ device, onBack }) {
  const [deviceData, setDeviceData] = useState(null);
  const [selectedRegister, setSelectedRegister] = useState(null);

  useEffect(() => {
    // Fetch full device data
    axios.get(`${API_URL}/api/devices/${device.id}`)
      .then(response => {
        setDeviceData(response.data);
      })
      .catch(error => {
        console.error('Error fetching device details:', error);
      });

    // Listen for real-time updates
    socket.on('data-update', (devices) => {
      const updatedDevice = devices.find(d => d.id === device.id);
      if (updatedDevice) {
        setDeviceData(updatedDevice);
      }
    });

    return () => {
      socket.off('data-update');
    };
  }, [device.id]);

  if (!deviceData) {
    return (
      <div className="device-detail">
        <button className="back-button" onClick={onBack}>← Back to Overview</button>
        <div>Loading device data...</div>
      </div>
    );
  }

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

  const getValueColor = (value, min, max) => {
    const range = max - min;
    const position = (value - min) / range;
    if (position > 0.9) return '#ff4444'; // Red for high
    if (position < 0.1) return '#ffaa00'; // Orange for low
    return '#2d7a2d'; // Green for normal
  };

  return (
    <div className="device-detail">
      <button className="back-button" onClick={onBack}>← Back to Overview</button>
      
      <div className="device-detail-header">
        <div className="device-detail-title">
          {getDeviceIcon(deviceData.deviceType)} {deviceData.name}
        </div>
        <div className="device-detail-subtitle">
          {deviceData.deviceType} • {deviceData.location}
        </div>
      </div>

      <div className="communication-config">
        <h3 style={{ color: '#ffffff', marginBottom: '15px' }}>RS-485 Communication Configuration</h3>
        <div className="config-grid">
          <div className="config-item">
            <span className="config-label">Modbus Slave ID</span>
            <span className="config-value">{deviceData.slaveId}</span>
          </div>
          <div className="config-item">
            <span className="config-label">Baud Rate</span>
            <span className="config-value">{deviceData.baudRate}</span>
          </div>
          <div className="config-item">
            <span className="config-label">Parity</span>
            <span className="config-value">{deviceData.parity}</span>
          </div>
          <div className="config-item">
            <span className="config-label">Stop Bits</span>
            <span className="config-value">{deviceData.stopBits}</span>
          </div>
          <div className="config-item">
            <span className="config-label">Protocol</span>
            <span className="config-value">Modbus RTU</span>
          </div>
          <div className="config-item">
            <span className="config-label">Topology</span>
            <span className="config-value">Daisy Chain</span>
          </div>
        </div>
      </div>

      <div className="registers-section">
        <h3>Holding Registers (Read/Write)</h3>
        <div className="registers-grid">
          {Object.entries(deviceData.registers.holding || {}).map(([address, register]) => (
            <div 
              key={address} 
              className="register-card"
              onClick={() => setSelectedRegister({ address, ...register, type: 'holding' })}
              style={{ cursor: 'pointer' }}
            >
              <div className="register-name">{register.name}</div>
              <div 
                className="register-value"
                style={{ color: getValueColor(register.value, register.min, register.max) }}
              >
                {register.value} {register.unit}
              </div>
              <div className="register-range">
                Range: {register.min} - {register.max} {register.unit}
              </div>
              <div className="register-range" style={{ fontSize: '10px', color: '#5a6f80' }}>
                Register: {address}
              </div>
            </div>
          ))}
        </div>
      </div>

      <div className="registers-section">
        <h3>Coils / Status Registers</h3>
        <div className="registers-grid">
          {Object.entries(deviceData.registers.coils || {}).map(([address, coil]) => (
            <div key={address} className="register-card">
              <div className="register-name">{coil.name}</div>
              <div className="coil-status">
                <div className={`coil-indicator ${coil.value ? 'active' : 'inactive'}`}></div>
                <span className="register-value" style={{ fontSize: '18px' }}>
                  {coil.value ? 'Active' : 'Inactive'}
                </span>
              </div>
              <div className="register-range" style={{ fontSize: '10px', color: '#5a6f80' }}>
                Coil: {address}
              </div>
            </div>
          ))}
        </div>
      </div>

      {selectedRegister && (
        <div style={{
          position: 'fixed',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          background: '#1a2332',
          border: '2px solid #3a5f8f',
          borderRadius: '12px',
          padding: '30px',
          zIndex: 1000,
          minWidth: '400px'
        }}>
          <h3 style={{ color: '#ffffff', marginBottom: '20px' }}>Register Details</h3>
          <div className="config-grid">
            <div className="config-item">
              <span className="config-label">Address</span>
              <span className="config-value">{selectedRegister.address}</span>
            </div>
            <div className="config-item">
              <span className="config-label">Name</span>
              <span className="config-value">{selectedRegister.name}</span>
            </div>
            <div className="config-item">
              <span className="config-label">Current Value</span>
              <span className="config-value">{selectedRegister.value} {selectedRegister.unit}</span>
            </div>
            <div className="config-item">
              <span className="config-label">Min</span>
              <span className="config-value">{selectedRegister.min} {selectedRegister.unit}</span>
            </div>
            <div className="config-item">
              <span className="config-label">Max</span>
              <span className="config-value">{selectedRegister.max} {selectedRegister.unit}</span>
            </div>
            <div className="config-item">
              <span className="config-label">Type</span>
              <span className="config-value">{selectedRegister.type === 'holding' ? 'Holding Register' : 'Coil'}</span>
            </div>
          </div>
          <button 
            className="back-button" 
            onClick={() => setSelectedRegister(null)}
            style={{ marginTop: '20px', width: '100%' }}
          >
            Close
          </button>
        </div>
      )}
    </div>
  );
}

export default DeviceDetail;

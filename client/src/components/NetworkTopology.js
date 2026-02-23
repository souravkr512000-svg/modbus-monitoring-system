import React from 'react';

function NetworkTopology({ devices }) {
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
    <div className="network-topology">
      <div className="topology-diagram">
        <div style={{ 
          color: '#ffffff', 
          fontSize: '18px', 
          fontWeight: '600',
          marginBottom: '20px',
          textAlign: 'center'
        }}>
          RS-485 Daisy Chain Network Topology
        </div>
        
        <div style={{
          display: 'flex',
          alignItems: 'center',
          width: '100%',
          gap: '20px',
          flexWrap: 'wrap',
          justifyContent: 'center'
        }}>
          <div style={{ 
            background: '#2a3441',
            padding: '15px 25px',
            borderRadius: '8px',
            border: '2px solid #3a5f8f',
            color: '#ffffff',
            fontWeight: '600'
          }}>
            Gateway/PLC
          </div>
          
          <div style={{ width: '30px', height: '3px', background: '#3a5f8f' }}></div>
          
          {devices.map((device, index) => (
            <React.Fragment key={device.id}>
              <div className="topology-device">
                <div className="connection-line"></div>
                <div className="device-icon">
                  {getDeviceIcon(device.deviceType)}
                </div>
                <div className="device-label">
                  <div style={{ fontWeight: '600', color: '#ffffff' }}>
                    Slave ID: {device.slaveId}
                  </div>
                  <div style={{ fontSize: '11px' }}>
                    {device.name}
                  </div>
                </div>
              </div>
              {index < devices.length - 1 && (
                <div style={{ width: '30px', height: '3px', background: '#3a5f8f' }}></div>
              )}
            </React.Fragment>
          ))}
        </div>
        
        <div style={{
          marginTop: '30px',
          padding: '15px',
          background: '#0f1620',
          borderRadius: '8px',
          border: '1px solid #3a5f8f',
          color: '#a0b0c0',
          fontSize: '13px',
          textAlign: 'center',
          maxWidth: '800px'
        }}>
          <div style={{ marginBottom: '10px', fontWeight: '600', color: '#ffffff' }}>
            Network Configuration
          </div>
          <div>Baud Rate: 9600 • Parity: Even • Stop Bits: 1 • Protocol: Modbus RTU</div>
          <div style={{ marginTop: '5px' }}>
            Termination Resistors: Enabled at end nodes • Half-Duplex Communication
          </div>
        </div>
      </div>
    </div>
  );
}

export default NetworkTopology;

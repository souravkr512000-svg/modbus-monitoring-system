import React, { useState, useEffect } from 'react';
import io from 'socket.io-client';
import axios from 'axios';
import './App.css';
import DeviceCard from './components/DeviceCard';
import DeviceDetail from './components/DeviceDetail';
import AlarmPanel from './components/AlarmPanel';
import NetworkTopology from './components/NetworkTopology';

const socket = io('http://localhost:3001');

function App() {
  const [devices, setDevices] = useState([]);
  const [selectedDevice, setSelectedDevice] = useState(null);
  const [alarms, setAlarms] = useState([]);
  const [connectionStatus, setConnectionStatus] = useState('Connecting...');

  useEffect(() => {
    // Fetch initial device list
    axios.get('http://localhost:3001/api/devices')
      .then(response => {
        setDevices(response.data);
      })
      .catch(error => {
        console.error('Error fetching devices:', error);
      });

    // Socket connection events
    socket.on('connect', () => {
      setConnectionStatus('Connected');
      console.log('Connected to Modbus server');
    });

    socket.on('disconnect', () => {
      setConnectionStatus('Disconnected');
      console.log('Disconnected from Modbus server');
    });

    socket.on('devices', (deviceData) => {
      setDevices(deviceData);
    });

    socket.on('data-update', (deviceData) => {
      setDevices(deviceData);
      
      // Check for alarms
      const newAlarms = [];
      deviceData.forEach(device => {
        Object.values(device.registers.coils || {}).forEach(coil => {
          if (coil.name.includes('Alarm') || coil.name.includes('Fault') || coil.name.includes('Trip')) {
            if (coil.value === true) {
              newAlarms.push({
                id: `${device.id}-${coil.name}-${Date.now()}`,
                deviceId: device.id,
                deviceName: device.name,
                alarmType: coil.name,
                timestamp: new Date().toISOString(),
                severity: coil.name.includes('Fault') || coil.name.includes('Trip') ? 'Critical' : 'Warning'
              });
            }
          }
        });
        
        // Check for out-of-range values
        Object.values(device.registers.holding || {}).forEach(reg => {
          if (reg.value > reg.max * 0.9 || reg.value < reg.min * 1.1) {
            newAlarms.push({
              id: `${device.id}-${reg.name}-${Date.now()}`,
              deviceId: device.id,
              deviceName: device.name,
              alarmType: `${reg.name} out of range: ${reg.value} ${reg.unit}`,
              timestamp: new Date().toISOString(),
              severity: reg.value > reg.max || reg.value < reg.min ? 'Critical' : 'Warning'
            });
          }
        });
      });
      
      if (newAlarms.length > 0) {
        setAlarms(prev => [...newAlarms, ...prev].slice(0, 50)); // Keep last 50 alarms
      }
    });

    return () => {
      socket.disconnect();
    };
  }, []);

  const handleDeviceSelect = (device) => {
    setSelectedDevice(device);
  };

  const handleBackToOverview = () => {
    setSelectedDevice(null);
  };

  return (
    <div className="App">
      <header className="app-header">
        <div className="header-content">
          <h1>🔌 Modbus/RS-485 Monitoring System</h1>
          <div className="header-info">
            <span className={`status-indicator ${connectionStatus === 'Connected' ? 'online' : 'offline'}`}>
              {connectionStatus}
            </span>
            <span className="device-count">{devices.length} Devices</span>
          </div>
        </div>
        <div className="header-subtitle">
          Industrial Device Integration - JSW & Amalgam Steel Experience
        </div>
      </header>

      <div className="main-container">
        {selectedDevice ? (
          <DeviceDetail 
            device={selectedDevice} 
            onBack={handleBackToOverview}
          />
        ) : (
          <>
            <div className="dashboard-section">
              <h2>Network Topology</h2>
              <NetworkTopology devices={devices} />
            </div>

            <div className="dashboard-section">
              <h2>Device Overview</h2>
              <div className="devices-grid">
                {devices.map(device => (
                  <DeviceCard
                    key={device.id}
                    device={device}
                    onClick={() => handleDeviceSelect(device)}
                  />
                ))}
              </div>
            </div>

            <div className="dashboard-section">
              <AlarmPanel alarms={alarms} devices={devices} />
            </div>
          </>
        )}
      </div>
    </div>
  );
}

export default App;

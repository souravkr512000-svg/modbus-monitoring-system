# Modbus/RS-485 Monitoring System

## Industrial Device Integration Experience Demo
**JSW & Amalgam Steel - Device Integration Project**

This application demonstrates real-world experience with RS-485/Modbus device integration for industrial monitoring systems, similar to work done in steel plants.

## Features

✅ **Multiple Device Types**
- Energy Meters (Voltage, Current, Power, PF monitoring)
- Temperature Monitoring Devices (Transformer winding/oil temperature)
- Protection Relays (Motor protection, fault detection)
- VFD Drives (Frequency, speed, torque control)

✅ **RS-485 Communication**
- Modbus RTU protocol simulation
- Daisy-chain network topology
- Configurable slave IDs, baud rates, parity
- Real-time data polling

✅ **Real-Time Monitoring Dashboard**
- Live data visualization
- Device status monitoring
- Network topology view
- Alarm and fault handling

✅ **Industrial-Grade Features**
- Register mapping (Holding registers, Coils)
- Communication status monitoring
- Out-of-range alarm detection
- Fault status tracking

## Technology Stack

- **Backend**: Node.js + Express + Socket.IO
- **Frontend**: React.js
- **Protocol**: Modbus RTU (simulated)
- **Communication**: RS-485 (simulated)

## Installation & Setup

### Prerequisites
- Node.js (v14 or higher)
- npm or yarn

### Step 1: Install Backend Dependencies
```bash
npm install
```

### Step 2: Install Frontend Dependencies
```bash
cd client
npm install
cd ..
```

### Step 3: Start the Application

**Option A: Run Both Servers Separately**

Terminal 1 - Backend Server:
```bash
npm start
```

Terminal 2 - Frontend (React):
```bash
cd client
npm start
```

**Option B: Development Mode (Auto-restart)**
```bash
npm run dev  # Backend with nodemon
# In another terminal:
cd client && npm start
```

### Step 4: Access the Application

- **Frontend Dashboard**: http://localhost:3000
- **Backend API**: http://localhost:3001

## Project Structure

```
modbus-monitoring-system/
├── server.js              # Backend server with Modbus simulation
├── package.json           # Backend dependencies
├── client/                # React frontend application
│   ├── src/
│   │   ├── App.js        # Main application component
│   │   ├── components/   # React components
│   │   │   ├── DeviceCard.js
│   │   │   ├── DeviceDetail.js
│   │   │   ├── AlarmPanel.js
│   │   │   └── NetworkTopology.js
│   │   └── ...
│   └── package.json      # Frontend dependencies
└── README.md
```

## Simulated Devices

### 1. Energy Meter - Main Panel
- **Slave ID**: 1
- **Parameters**: Voltage (L1-L2, L2-L3, L3-L1), Current (L1, L2, L3), Active/Reactive Power, Power Factor, Frequency

### 2. Temperature Monitor - Transformer
- **Slave ID**: 2
- **Parameters**: Winding Temperatures, Oil Temperature, Ambient Temperature

### 3. Protection Relay - Motor 1
- **Slave ID**: 3
- **Parameters**: Motor Current, Voltage, Power, Running Hours, Fault Status

### 4. VFD Drive - Conveyor Belt
- **Slave ID**: 4
- **Parameters**: Output Frequency, Current, Voltage, DC Bus Voltage, Motor Speed, Torque

### 5. Energy Meter - Sub Panel
- **Slave ID**: 5
- **Parameters**: Similar to Main Panel Energy Meter

## API Endpoints

### GET `/api/devices`
Returns list of all devices with basic information.

### GET `/api/devices/:id`
Returns detailed information for a specific device.

### GET `/api/devices/:id/registers`
Returns all registers (holding + coils) for a device.

### POST `/api/devices/:id/read-register`
Reads a specific register from a device.
```json
{
  "registerType": "holding",
  "address": 40001
}
```

## WebSocket Events

### Client → Server
- `connect` - Establishes connection

### Server → Client
- `devices` - Initial device list
- `data-update` - Real-time data updates (every 2 seconds)

## Interview Talking Points

### Technical Experience Demonstrated:

1. **RS-485 Communication Setup**
   - Configured daisy-chain topology
   - Set baud rates, parity, stop bits
   - Assigned unique Modbus slave IDs

2. **Modbus Protocol Understanding**
   - Holding registers for measurements
   - Coils for status/alarms
   - Register mapping and addressing

3. **Device Integration**
   - Multiple device types (meters, sensors, relays, drives)
   - Real-time data collection
   - Communication validation

4. **Monitoring & Visualization**
   - Live dashboard for operators
   - Alarm generation and handling
   - Fault detection and reporting

5. **Troubleshooting**
   - Communication loss detection
   - Data validation
   - Register mapping verification

## Key Features to Highlight in Interview

✅ **Real Industrial Devices**: Energy meters, temperature monitors, protection relays, VFDs  
✅ **RS-485/Modbus**: Standard industrial communication protocol  
✅ **Network Topology**: Daisy-chain configuration  
✅ **Real-Time Monitoring**: Live data updates and visualization  
✅ **Alarm System**: Fault detection and alerting  
✅ **Device Configuration**: Slave IDs, baud rates, register mapping  

## Notes

- This is a **simulation** for demonstration purposes
- In real industrial environments, you would use actual Modbus libraries (e.g., `modbus-serial` npm package)
- Data values simulate realistic variations
- Communication parameters match industry standards

## License

MIT

---

**Built for demonstrating industrial Modbus/RS-485 device integration experience**

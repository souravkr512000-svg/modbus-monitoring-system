const express = require('express');
const http = require('http');
const socketIo = require('socket.io');
const cors = require('cors');
const path = require('path');

const app = express();
const server = http.createServer(app);
const io = socketIo(server, {
  cors: {
    origin: "*",
    methods: ["GET", "POST"]
  }
});

app.use(cors());
app.use(express.json());

// Serve static files from React app in production
if (process.env.NODE_ENV === 'production') {
  app.use(express.static(path.join(__dirname, 'client/build')));
}

// Simulated Modbus Devices Configuration
// Each device has: slaveId, deviceType, registers (holding/input/coils)
const devices = [
  {
    id: 1,
    name: "Energy Meter - Main Panel",
    slaveId: 1,
    deviceType: "Energy Meter",
    location: "Main Electrical Panel",
    baudRate: 9600,
    parity: "Even",
    stopBits: 1,
    registers: {
      holding: {
        40001: { name: "Voltage L1-L2", value: 415, unit: "V", min: 380, max: 440 },
        40002: { name: "Voltage L2-L3", value: 412, unit: "V", min: 380, max: 440 },
        40003: { name: "Voltage L3-L1", value: 418, unit: "V", min: 380, max: 440 },
        40004: { name: "Current L1", value: 245, unit: "A", min: 0, max: 500 },
        40005: { name: "Current L2", value: 238, unit: "A", min: 0, max: 500 },
        40006: { name: "Current L3", value: 252, unit: "A", min: 0, max: 500 },
        40007: { name: "Active Power", value: 175, unit: "kW", min: 0, max: 300 },
        40008: { name: "Reactive Power", value: 45, unit: "kVAR", min: 0, max: 150 },
        40009: { name: "Power Factor", value: 0.92, unit: "", min: 0, max: 1 },
        40010: { name: "Frequency", value: 50.02, unit: "Hz", min: 49.5, max: 50.5 }
      },
      coils: {
        1: { name: "Alarm Status", value: false },
        2: { name: "Communication OK", value: true }
      }
    }
  },
  {
    id: 2,
    name: "Temperature Monitor - Transformer",
    slaveId: 2,
    deviceType: "Temperature Monitor",
    location: "Transformer Bay",
    baudRate: 9600,
    parity: "Even",
    stopBits: 1,
    registers: {
      holding: {
        40001: { name: "Winding Temp 1", value: 68, unit: "°C", min: 0, max: 120 },
        40002: { name: "Winding Temp 2", value: 72, unit: "°C", min: 0, max: 120 },
        40003: { name: "Oil Temperature", value: 55, unit: "°C", min: 0, max: 100 },
        40004: { name: "Ambient Temperature", value: 32, unit: "°C", min: 0, max: 50 }
      },
      coils: {
        1: { name: "High Temp Alarm", value: false },
        2: { name: "Communication OK", value: true },
        3: { name: "Fault Status", value: false }
      }
    }
  },
  {
    id: 3,
    name: "Protection Relay - Motor 1",
    slaveId: 3,
    deviceType: "Protection Relay",
    location: "Motor Control Center",
    baudRate: 9600,
    parity: "Even",
    stopBits: 1,
    registers: {
      holding: {
        40001: { name: "Motor Current", value: 85, unit: "A", min: 0, max: 150 },
        40002: { name: "Motor Voltage", value: 415, unit: "V", min: 0, max: 500 },
        40003: { name: "Motor Power", value: 55, unit: "kW", min: 0, max: 100 },
        40004: { name: "Running Hours", value: 12450, unit: "Hrs", min: 0, max: 99999 }
      },
      coils: {
        1: { name: "Overload Trip", value: false },
        2: { name: "Earth Fault", value: false },
        3: { name: "Communication OK", value: true },
        4: { name: "Motor Running", value: true }
      }
    }
  },
  {
    id: 4,
    name: "VFD Drive - Conveyor Belt",
    slaveId: 4,
    deviceType: "VFD Drive",
    location: "Production Line 2",
    baudRate: 9600,
    parity: "Even",
    stopBits: 1,
    registers: {
      holding: {
        40001: { name: "Output Frequency", value: 45.5, unit: "Hz", min: 0, max: 50 },
        40002: { name: "Output Current", value: 125, unit: "A", min: 0, max: 200 },
        40003: { name: "Output Voltage", value: 380, unit: "V", min: 0, max: 500 },
        40004: { name: "DC Bus Voltage", value: 540, unit: "V", min: 0, max: 600 },
        40005: { name: "Motor Speed", value: 1365, unit: "RPM", min: 0, max: 1500 },
        40006: { name: "Torque", value: 78, unit: "%", min: 0, max: 100 }
      },
      coils: {
        1: { name: "Fault Status", value: false },
        2: { name: "Communication OK", value: true },
        3: { name: "Drive Running", value: true },
        4: { name: "Overcurrent Alarm", value: false }
      }
    }
  },
  {
    id: 5,
    name: "Energy Meter - Sub Panel",
    slaveId: 5,
    deviceType: "Energy Meter",
    location: "Sub Station 1",
    baudRate: 9600,
    parity: "Even",
    stopBits: 1,
    registers: {
      holding: {
        40001: { name: "Voltage L1-L2", value: 410, unit: "V", min: 380, max: 440 },
        40002: { name: "Voltage L2-L3", value: 408, unit: "V", min: 380, max: 440 },
        40003: { name: "Voltage L3-L1", value: 412, unit: "V", min: 380, max: 440 },
        40004: { name: "Current L1", value: 185, unit: "A", min: 0, max: 400 },
        40005: { name: "Current L2", value: 178, unit: "A", min: 0, max: 400 },
        40006: { name: "Current L3", value: 192, unit: "A", min: 0, max: 400 },
        40007: { name: "Active Power", value: 125, unit: "kW", min: 0, max: 250 },
        40008: { name: "Reactive Power", value: 32, unit: "kVAR", min: 0, max: 100 },
        40009: { name: "Power Factor", value: 0.94, unit: "", min: 0, max: 1 },
        40010: { name: "Frequency", value: 49.98, unit: "Hz", min: 49.5, max: 50.5 }
      },
      coils: {
        1: { name: "Alarm Status", value: false },
        2: { name: "Communication OK", value: true }
      }
    }
  }
];

// Simulate realistic data variations
function simulateDataVariation(device) {
  const variations = device.registers.holding;
  
  Object.keys(variations).forEach(regAddr => {
    const reg = variations[regAddr];
    const range = reg.max - reg.min;
    const variation = (Math.random() - 0.5) * range * 0.05; // ±2.5% variation
    reg.value = Math.max(reg.min, Math.min(reg.max, reg.value + variation));
    
    // Round to appropriate decimal places
    if (reg.unit === "V" || reg.unit === "A" || reg.unit === "kW" || reg.unit === "kVAR" || reg.unit === "°C" || reg.unit === "RPM") {
      reg.value = Math.round(reg.value);
    } else if (reg.unit === "Hz" || reg.unit === "") {
      reg.value = Math.round(reg.value * 100) / 100;
    }
    
    // Simulate occasional alarms
    if (Math.random() < 0.02) { // 2% chance
      if (reg.value > reg.max * 0.9) {
        device.registers.coils[1].value = true; // Alarm
      }
    }
  });
  
  // Simulate communication loss occasionally
  if (Math.random() < 0.01) { // 1% chance
    device.registers.coils[2].value = false; // Communication lost
  } else {
    device.registers.coils[2].value = true; // Communication OK
  }
}

// API Routes
app.get('/api/devices', (req, res) => {
  res.json(devices.map(d => ({
    id: d.id,
    name: d.name,
    slaveId: d.slaveId,
    deviceType: d.deviceType,
    location: d.location,
    baudRate: d.baudRate,
    parity: d.parity,
    stopBits: d.stopBits,
    status: d.registers.coils[2]?.value ? 'Online' : 'Offline'
  })));
});

app.get('/api/devices/:id', (req, res) => {
  const device = devices.find(d => d.id === parseInt(req.params.id));
  if (!device) {
    return res.status(404).json({ error: 'Device not found' });
  }
  res.json(device);
});

app.get('/api/devices/:id/registers', (req, res) => {
  const device = devices.find(d => d.id === parseInt(req.params.id));
  if (!device) {
    return res.status(404).json({ error: 'Device not found' });
  }
  res.json({
    holding: device.registers.holding,
    coils: device.registers.coils
  });
});

app.post('/api/devices/:id/read-register', (req, res) => {
  const device = devices.find(d => d.id === parseInt(req.params.id));
  if (!device) {
    return res.status(404).json({ error: 'Device not found' });
  }
  
  const { registerType, address } = req.body;
  const register = device.registers[registerType]?.[address];
  
  if (!register) {
    return res.status(404).json({ error: 'Register not found' });
  }
  
  res.json({
    slaveId: device.slaveId,
    registerType,
    address,
    value: register.value,
    name: register.name,
    unit: register.unit || ''
  });
});

// Serve React app in production
if (process.env.NODE_ENV === 'production') {
  // Serve static files from React app
  app.use(express.static(path.join(__dirname, 'client/build')));
  
  // Catch all handler: send back React's index.html file for any non-API routes
  app.get('*', (req, res) => {
    // Don't serve index.html for API routes
    if (req.path.startsWith('/api')) {
      return res.status(404).json({ error: 'API endpoint not found' });
    }
    res.sendFile(path.join(__dirname, 'client/build/index.html'));
  });
}

// WebSocket connection for real-time updates
io.on('connection', (socket) => {
  console.log('Client connected:', socket.id);
  
  socket.emit('devices', devices);
  
  // Send periodic updates every 2 seconds
  const interval = setInterval(() => {
    devices.forEach(device => {
      simulateDataVariation(device);
    });
    socket.emit('data-update', devices);
  }, 2000);
  
  socket.on('disconnect', () => {
    console.log('Client disconnected:', socket.id);
    clearInterval(interval);
  });
});

const PORT = process.env.PORT || 3001;
server.listen(PORT, () => {
  console.log(`Modbus Monitoring Server running on port ${PORT}`);
  if (process.env.NODE_ENV === 'production') {
    console.log(`Production mode: Serving React app`);
  } else {
    console.log(`Development mode: React app runs separately on port 3000`);
  }
});

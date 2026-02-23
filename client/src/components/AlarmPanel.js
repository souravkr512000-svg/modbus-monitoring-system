import React from 'react';

function AlarmPanel({ alarms, devices }) {
  const getDeviceName = (deviceId) => {
    const device = devices.find(d => d.id === deviceId);
    return device ? device.name : 'Unknown Device';
  };

  const formatTime = (timestamp) => {
    const date = new Date(timestamp);
    return date.toLocaleTimeString();
  };

  return (
    <div className="alarm-panel">
      <h3>Alarm & Fault Status</h3>
      <div className="alarm-list">
        {alarms.length === 0 ? (
          <div className="no-alarms">No active alarms</div>
        ) : (
          alarms.map(alarm => (
            <div key={alarm.id} className={`alarm-item ${alarm.severity.toLowerCase()}`}>
              <div className="alarm-header">
                <span className="alarm-type">{alarm.alarmType}</span>
                <span className="alarm-time">{formatTime(alarm.timestamp)}</span>
              </div>
              <div className="alarm-device">{getDeviceName(alarm.deviceId)}</div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

export default AlarmPanel;

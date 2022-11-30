/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from 'react';

export default function Timer() {
  const [seconds, setSeconds] = useState(0);
  const [minutes, setMinutes] = useState(0);
  const [hours, setHours] = useState(0);
  const [isActive, setIsActive] = useState(false);

  const startTimer = () => {};

  function toggleActive() {
    setIsActive(!isActive);
  }

  function reset() {
    setSeconds(0);
    setMinutes(0);
    setHours(0);
    setIsActive(false);
  }

  function setTime(e) {
    let time = parseInt(e, 10);
    setHours(time % 3600);
    time -= hours * 3600;
    setMinutes(time % 60);
    time -= minutes * 60;
    setSeconds(time);
  }

  const handleKeyPress = (e) => {
    const char = parseInt(e.target.value, 10);
    if (e.key === 'Enter' || e.key === 'Space') {
      toggleActive();
    } else if (char <= 9 && char >= 0) {
      setSeconds(char);
    }
  };

  const onlyNumbers = (e) => {
    //   const regex = /^[0-9\b]+$/;

    //   if (e.target.value === '' || regex.test(e.target.value)) {
    //     setSeconds(parseInt(e.target.value, 10));
    //   }
    const char = parseInt(e.target.value, 10);
    if (char <= 9 && char >= 0) {
      setTime(char);
    }
  };

  useEffect(() => {
    let interval = null;
    if (isActive) {
      interval = setInterval(() => {
        setSeconds(seconds - 1);
        if (seconds === 1) {
          if (minutes === 0) {
            if (hours === 0) {
              clearInterval(interval);
              toggleActive();
            } else {
              setHours(hours - 1);
              setMinutes(59);
              setSeconds(59);
            }
          } else {
            setMinutes(minutes - 1);
            setSeconds(59);
          }
        }
      }, 1000);
    }
    return () => clearInterval(interval);
  });

  return (
    <div className="bg-grey">
      <div>
        <textarea
          type="number"
          maxLength={2}
          // className={`text-${isActive ? disabled : true}`}
          onKeyPress={handleKeyPress}
          onChange={(e) => setSeconds(e.target.value)}>
          00
        </textarea>
      </div>
      <div className="time">
        <h1>
          {hours < 10 ? `0${hours}` : hours}:{minutes < 10 ? `0${minutes}` : minutes}:
          {seconds < 10 ? `0${seconds}` : seconds}
        </h1>
      </div>
      <div>
        <button
          type="button"
          className={`button-primary-${isActive ? 'active' : 'inactive'}`}
          onClick={toggleActive}>
          {isActive ? 'Pause' : 'Start'}
        </button>
        <button type="button" className="secondary" onClick={reset}>
          Reset
        </button>
      </div>
    </div>
  );
}

// import React, { useState, useRef } from 'react';

// export default function CountdownTimer() {
//   const Ref = useRef(null);
//   const [timer, setTimer] = useState('00:00:00');

//   const getTimeRemaining = (e) => {
//     const total = Date.parse(e) - Date.parse(new Date());
//     const seconds = Math.floor((total / 1000) % 60);
//     const minutes = Math.floor((total / 1000 / 60) % 60);
//     const hours = Math.floor((total / 1000 / 60 / 60) % 24);
//     return { total, hours, minutes, seconds };
//   };

//   const startTimer = (e) => {
//     const { total, hours, minutes, seconds } = getTimeRemaining(e);
//     if (total >= 0) {
//       setTimer(
//         `${hours > 9 ? hours : `0${hours}`}:${minutes > 9 ? minutes : `0${minutes}`}:${
//           seconds > 9 ? seconds : `0${seconds}`
//         }`
//       );
//     }
//   };

//   const clearTimer = (e) => {
//     setTimer('00:05:00');

//     if (Ref.current) clearInterval(Ref.current);
//     const id = setInterval(() => {
//       startTimer(e);
//     }, 1000);
//     Ref.current = id;
//   };

//   const getDeadTime = () => {
//     const deadline = new Date();

//     deadline.setSeconds(deadline.getSeconds() + 300);
//     return deadline;
//   };

//   const resetTimer = () => {
//     clearTimer(getDeadTime());
//   };

//   return (
//     <div>
//       <h2>{timer}</h2>
//       <input type="time" />
//       <button type="button" onClick={resetTimer}>
//         Reset
//       </button>
//     </div>
//   );
// }

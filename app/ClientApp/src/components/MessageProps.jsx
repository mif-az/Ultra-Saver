import React from 'react';
import MessageBox from './MessageBox';

document.getElementById('NAME').innerHTML = 'Name';
const date = new Date();
document.getElementById('DATE').innerHTML = date.getHours();
document.getElementById('TEXT').innerHTML = 'labas';

export default function MesageProps() {
  return (
    <div>
      <h3 id="NAME"> </h3>
      <p id="DATE"> </p>
      <p id="TEXT"> </p>
    </div>
  );
}

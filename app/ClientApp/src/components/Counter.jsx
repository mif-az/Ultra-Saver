/* eslint-disable react/destructuring-assignment */
/* eslint-disable import/prefer-default-export */
import React, { Component } from 'react';

export class Counter extends Component {
  // eslint-disable-next-line react/static-property-placement
  static displayName = Counter.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
  }

  incrementCounter() {
    this.setState({
      // eslint-disable-next-line react/no-access-state-in-setstate
      currentCount: this.state.currentCount + 1
    });
  }

  render() {
    return (
      <div>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>

        <p aria-live="polite">
          Current count: <strong>{this.state.currentCount}</strong>
        </p>

        <button type="button" className="btn btn-primary" onClick={this.incrementCounter}>
          Increment
        </button>
      </div>
    );
  }
}
